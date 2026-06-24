using OpenAI;
using OpenAI.Embeddings;
using System.ClientModel;
using System.Text.Json;

namespace CalamityRAGChatPlugin;

internal class Embedding
{
    private readonly Uri endpoint;
    private readonly EmbeddingClient client;
    /// <summary>
    /// 对Json的Description进行嵌入编码
    /// </summary>
    public Embedding(Uri embeddingModelEndpoint, ApiKeyCredential? aikey = null)
    {
        var options = new OpenAIClientOptions()
        {
            Endpoint = embeddingModelEndpoint
        };
        endpoint = embeddingModelEndpoint;
        client = new EmbeddingClient("Qwen3-Embedding-0.6B", aikey ?? new ApiKeyCredential("null"), options);
    }

    /// <summary>
    /// 获取给定json字符串中的description属性，如果没有，则对整个Json进行嵌入编码
    /// </summary>
    public async Task<float[]> GetJsonDocumentFloats(string jsonDocument, string propName = "description")
    {
        var jsond = JsonDocument.Parse(jsonDocument);
        if (jsond.RootElement.TryGetProperty(propName, out var propValue)) {
            try {
                var str = propValue.GetString();
                Console.WriteLine($"对内容进行嵌入编码: {str}");
                if (str != null) {
                    var embeddingValue = await client.GenerateEmbeddingAsync(str);
                    return embeddingValue.Value.ToFloats().ToArray();
                }
            } catch { }
        }
        var eValue = await client.GenerateEmbeddingAsync(jsonDocument);
        return eValue.Value.ToFloats().ToArray();
        //throw new JsonException($"未找到属性: {propName}");
    }

    /// <summary>
    /// 获取给定字符串的嵌入向量值
    /// </summary>
    public async Task<float[]> GetStringFloatsAsync(string value)
    {
        var eValue = await client.GenerateEmbeddingAsync(value);
        return eValue.Value.ToFloats().ToArray();
    }
}

/// 将给定的内容进行输入到嵌入模型
//static async Task<float[]> GetEmbedding(string value)
//    {
//        var options = new OpenAIClientOptions()
//        {
//            Endpoint = new Uri("http://localhost:8080/v1/embeddings")
//        };

//        var client = new EmbeddingClient("Qwen3-Embedding-0.6B", new ApiKeyCredential("null"), options);
//        var embedding = await client.GenerateEmbeddingAsync(value);
//        return embedding.Value.ToFloats().ToArray();
//    }
