using OpenAI;
using OpenAI.Chat;
using Qdrant.Client;
using System.ClientModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CalamityRAGChatPlugin;

/// <summary>
/// 建议一次问答创建一个. 没做上下文管理
/// </summary>
internal class RAGChatClient
{
    private readonly ChatClient _chatClient;
    private readonly QdrantClient _qdrantClient;
    private readonly ChatCompletionOptions _chatCompletionOptions;
    private readonly string _qdrantCollectionName;
    private readonly Embedding _embeddingClient;
    public ulong searchLimit = 5;
    private readonly ChatMessage systemChatMessage;
    private readonly List<ChatMessage> _content = [];
    private readonly ushort _maxToolCallCount = 3;

    public RAGChatClient(
        string modle,
        ApiKeyCredential key,
        OpenAIClientOptions clientOptions,
        Embedding embedding,
        QdrantClient qdrantClient,
        string qdrantCollectionName,
        ChatCompletionOptions? completionOptions = null,
        ulong searchLimit = 4,
        ushort maxToolCallCount = 3,
        string systemPm = "系统提示词")
    {
        _maxToolCallCount = maxToolCallCount;
        systemChatMessage = ChatMessage.CreateSystemMessage(systemPm);
        _content.Add(systemChatMessage);
        this.searchLimit = searchLimit;
        _embeddingClient = embedding;
        _chatClient = new ChatClient(modle, key, clientOptions);
        _qdrantClient = qdrantClient;
        this._qdrantCollectionName = qdrantCollectionName;
        completionOptions ??= new ChatCompletionOptions() 
        { 
            MaxOutputTokenCount = 1000
        };
        _chatCompletionOptions = completionOptions;
        _chatCompletionOptions.Tools.Add(ChatTool.CreateFunctionTool(
            functionName: nameof(VectorRetrievalCalamityModContent),
            functionDescription: "从 Calamity Mod 的知识库中检索相关内容。当需要查找灾厄模组的具体物品、Boss、机制、合成配方或背景故事时，请使用此工具。",
            functionParameters: BinaryData.FromObjectAsJson(new
            {
                type = "object",
                properties = new
                {
                    content = new
                    {
                        type = "string",
                        description = "要检索的查询内容，可以是关键词、问题或描述。"
                    }
                },
                required = new[] { "content" }
            }
        )));
    }

    public async Task<string> VectorRetrievalCalamityModContent(string content)
    {
        var searchResult = await _qdrantClient.SearchAsync(
            collectionName: _qdrantCollectionName, 
            vector: await _embeddingClient.GetStringFloatsAsync(content),
            limit: searchLimit);

        var searchResultString = new StringBuilder();
        foreach (var point in searchResult) {
            searchResultString.AppendLine(JsonSerializer.Serialize(point.Payload));
        }
        return searchResultString.ToString();
    }

    public async Task<string> ChatAsync(string inputContent)
    {
        var userInput = ChatMessage.CreateUserMessage(inputContent);
        _content.Add(userInput);
        return await ChatWithToolLoopAsync(0);
    }

    private async Task<string> ChatWithToolLoopAsync(int toolCallCount) // 递归
    {
        var chatValue = await _chatClient.CompleteChatAsync(_content, _chatCompletionOptions);
        var chatCompletion = chatValue.Value;
        switch (chatCompletion.FinishReason) {
            case ChatFinishReason.Stop: // 正常结束
                _content.Add(ChatMessage.CreateAssistantMessage(chatCompletion));
                return chatCompletion.Content[0].Text;
            case ChatFinishReason.Length: // 长度过长
                return "上下文过长";
            case ChatFinishReason.ContentFilter: // 触发过滤规则
                return "触发了模型过滤规则";
            case ChatFinishReason.ToolCalls: // 工具调用
                _content.Add(ChatMessage.CreateAssistantMessage(chatCompletion));
                toolCallCount++;
                foreach (var chatToolCall in chatCompletion.ToolCalls) {
                    if (toolCallCount > _maxToolCallCount) {
                        _content.Add(ChatMessage.CreateToolMessage(
                            chatToolCall.Id, 
                            "工具调用次数太多了，该生成回答了！"));
                        continue;
                    }
                    switch (chatToolCall.FunctionName) {
                        case nameof(VectorRetrievalCalamityModContent):
                            var toolCallValue = "工具调用失败";
                            try {
                                var content = chatToolCall.FunctionArguments.ToObjectFromJson<VectorRetrievalCalamityModContentArgument>()!.Content;
                                toolCallValue = await VectorRetrievalCalamityModContent(content);
                            } catch { }
                            _content.Add(ChatMessage.CreateToolMessage(chatToolCall.Id, toolCallValue));
                            break;
                    }
                }
                return await ChatWithToolLoopAsync(toolCallCount);
            case ChatFinishReason.FunctionCall: // 已弃用
                return "返回了已弃用的FunctionCall";
            default:
                return "无效的响应";
        }
    }

    public class VectorRetrievalCalamityModContentArgument
    {
        [JsonPropertyName("content")]
        public string Content { get; set; } = "";
    }
}
