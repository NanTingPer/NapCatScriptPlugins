using NapCatScript.Core;
using NapCatScript.Core.Model;
using NapCatScript.Core.Services;
using OpenAI;
using Qdrant.Client;
using System.ClientModel;
using static NapCatScript.Core.MsgHandle.Utils;
using static NapCatScript.Start.Main_;

namespace CalamityRAGChatPlugin;

public class CalamityRAGChatPlugin : PluginType
{
    public CalamityRAGChatPlugin()
    {
        
    }

    /// <summary>
    /// 模型的访问key
    /// </summary>
    private readonly string SKNAME = "CalamityRAGKey";

    // 持久化客户端（插件级别复用）
    private QdrantClient qdrantClient;
    private Embedding embeddingClient;
    private string apiKey;

    // 命令前缀
    private const string COMMAND_PREFIX = "CRC";
    private const string HELP_COMMAND = "CRCHelp";

    public override void Init()
    {
        // 初始化配置
        CRCConfig.Init();

        // 初始化持久化客户端
        InitializePersistentClients();
    }

    /// <summary>
    /// 初始化持久化客户端（QdrantClient 和 Embedding）
    /// </summary>
    private void InitializePersistentClients()
    {
        try {
            apiKey = Config.GetConf(SKNAME) ?? "";

            // 初始化 Embedding 客户端（持久化）
            string embeddingUri = CRCConfig.EmbeddingUri;
            var embeddingEndpoint = string.IsNullOrEmpty(embeddingUri)
                ? new Uri("http://localhost:8080/v1")
                : new Uri(embeddingUri);
            embeddingClient = new Embedding(embeddingEndpoint, new ApiKeyCredential(apiKey));

            // 初始化 Qdrant 客户端（持久化）
            string vectorStoreUri = CRCConfig.VectorStoreUri;
            if (string.IsNullOrEmpty(vectorStoreUri)) {
                Log.InstanceLog.Waring("向量库 URI 未配置");
                return;
            }
            qdrantClient = new QdrantClient(new Uri(vectorStoreUri));
        } catch (Exception ex) {
            Log.InstanceLog.Erro($"初始化持久化客户端失败: {ex.Message}", ex.StackTrace);
        }
    }

    /// <summary>
    /// 创建新的 RAGChatClient 实例（每个会话一个）
    /// </summary>
    private RAGChatClient? CreateRAGChatClient()
    {
        if (qdrantClient == null || embeddingClient == null || string.IsNullOrEmpty(apiKey)) {
            return null;
        }

        return new RAGChatClient(
            modle: CRCConfig.ModelName,
            key: new ApiKeyCredential(apiKey),
            clientOptions: new OpenAIClientOptions() { Endpoint = new Uri(CRCConfig.AIEndpoint) },
            embedding: embeddingClient,
            qdrantClient: qdrantClient,
            qdrantCollectionName: "Calamity_Mod",
            systemPm: CRCConfig.Prompt
        );
    }

    public override async Task Run(MsgInfo msg, string httpUri)
    {
        string messageContent = msg.MessageContent.Trim();

        // 检查是否是 CRC 命令
        if (messageContent.StartsWith(COMMAND_PREFIX)) {
            await HandleCommand(msg, messageContent);
            return;
        }

        // 检查是否需要创建会话（仅当消息以 CRCStartString 开头时）
        if (!messageContent.StartsWith($"[CQ:at,qq={BotId}]")) {
            return; // 不匹配启动字符串，不创建会话
        }

        await RAGChat(msg, httpUri);
    }

    #region 命令系统

    /// <summary>
    /// 处理 CRC 命令
    /// </summary>
    private async Task HandleCommand(MsgInfo msg, string command)
    {
        // 权限验证：只有管理员可以执行命令
        if (!CRCConfig.IsAdmin(msg.UserId)) {
            //SendTextAsync(msg, HttpUri, "权限不足，只有管理员可以执行此命令。", CTokrn);
            return;
        }

        // 解析命令
        string[] parts = command.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
        string cmdName = parts[0];
        string cmdArg = parts.Length > 1 ? parts[1] : "";

        switch (cmdName) {
            case HELP_COMMAND:
                HandleHelpCommand(msg);
                break;
            case "CRCSetVectorStoreUri":
                HandleSetConfig(msg, CRCConfig.CRCVectorStoreUri, cmdArg, "向量库URI");
                break;
            case "CRCSetEmbeddingUri":
                HandleSetConfig(msg, CRCConfig.CRCEmbeddingUri, cmdArg, "Embedding URI");
                break;
            case "CRCSetAdminUsers":
                HandleSetConfig(msg, CRCConfig.CRCAdminUsers, cmdArg, "管理员用户列表");
                break;
            case "CRCSetPromp":
                HandleSetConfig(msg, CRCConfig.CRCAIPromp, cmdArg, "AI提示词模板");
                break;
            case "CRCSetCRCStartString":
                HandleSetConfig(msg, CRCConfig.CRCStartString, cmdArg, "启动字符串");
                break;
            case "CRCGetConfig":
                HandleGetConfig(msg);
                break;
            case "CRCRefreshConfig":
                HandleRefreshConfig(msg);
                break;
            default:
                SendTextAsync(msg, HttpUri, $"未知命令: {cmdName}\n输入 CRCHelp 查看帮助。", CTokrn);
                break;
        }
    }

    /// <summary>
    /// 处理 CRCHelp 命令
    /// </summary>
    private void HandleHelpCommand(MsgInfo msg)
    {
        string helpText = $"""
        === CRC 插件命令帮助 ===
        【配置设置命令】（需要管理员权限）
        • CRCSetVectorStoreUri <URI> - 设置向量库URI
        • CRCSetEmbeddingUri <URI> - 设置Embedding URI
        • CRCSetAdminUsers <QQ1,QQ2,...> - 设置管理员用户列表（逗号分隔）
        • CRCSetPrompt <提示词> - 设置系统提示词
        • CRCSetCRCStartString <字符串> - 设置会话启动字符串

        【查询命令】
        • CRCGetConfig - 查看当前所有配置
        • CRCRefreshConfig - 刷新配置缓存

        【其他】
        • CRCHelp - 显示此帮助信息

        【会话启动】
        发送消息时，消息内容需以启动字符串开头才会创建RAG会话。

        【示例】
        CRCSetVectorStoreUri http://localhost:8080/api/vectors
        CRCSetAdminUsers 123456789,987654321
        CRCSetCRCStartString !对话
        """;

        SendTextAsync(msg, HttpUri, helpText, CTokrn);
    }

    /// <summary>
    /// 处理设置配置命令
    /// </summary>
    private void HandleSetConfig(MsgInfo msg, string configKey, string value, string displayName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            SendTextAsync(msg, HttpUri, $"请提供{displayName}的值。\n示例: CRCSet{configKey} <值>", CTokrn);
            return;
        }

        bool success = CRCConfig.SetConf(configKey, value);
        if (success)
        {
            SendTextAsync(msg, HttpUri, $"{displayName}已设置为: {value}", CTokrn);
        }
        else
        {
            SendTextAsync(msg, HttpUri, $"设置{displayName}失败，请稍后重试。", CTokrn);
        }
    }

    /// <summary>
    /// 处理查看配置命令
    /// </summary>
    private void HandleGetConfig(MsgInfo msg)
    {
        string configInfo = $"""
        === 当前配置 ===
        【向量库URI】
        {CRCConfig.VectorStoreUri}

        【Embedding URI】
        {CRCConfig.EmbeddingUri}

        【管理员用户列表】
        {string.Join(", ", CRCConfig.AdminUsers)}

        【系统提示词】
        {CRCConfig.Prompt}

        【启动字符串】
        {CRCConfig.StartString}
        """;

        SendTextAsync(msg, HttpUri, configInfo, CTokrn);
    }

    /// <summary>
    /// 处理刷新配置命令
    /// </summary>
    private void HandleRefreshConfig(MsgInfo msg)
    {
        CRCConfig.Refresh();
        InitializePersistentClients(); // 重新初始化持久化客户端
        SendTextAsync(msg, HttpUri, "配置已刷新。", CTokrn);
    }

    #endregion

    #region RAG 聊天

    private async Task RAGChat(MsgInfo msg, string httpUri)
    {
        try {
            // 每次会话创建新的 RAGChatClient
            var ragChatClient = CreateRAGChatClient();
            if (ragChatClient == null) {
                SendTextAsync(msg, httpUri, "RAG 客户端未初始化，请先配置 API Key 和向量库。", CTokrn);
                return;
            }

            // 从用户消息中获取输入（去除启动字符串）
            string userInput = msg.MessageContent.Trim();
            if (userInput.StartsWith(CRCConfig.StartString)) {
                userInput = userInput.Substring(CRCConfig.StartString.Length).Trim();
            }

            // 使用 RAGChatClient 进行对话
            var response = await ragChatClient.ChatAsync(userInput);

            // 回复用户
            SendTextAsync(msg, httpUri, response, CTokrn);
        } catch (Exception ex) {
            Log.InstanceLog.Erro($"RAG聊天出错: {ex.Message}", ex.StackTrace);
            SendTextAsync(msg, httpUri, $"RAG聊天出错: {ex.Message}", CTokrn);
        }
    }

    #endregion
}
