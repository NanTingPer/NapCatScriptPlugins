using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiscordToQQ.JsonModel.Get;

/// <summary>
/// /channels/{channel.id}/messages/{message.id}
/// </summary>
public class GetChannelMessages
{
    /// <summary>
    /// 获取关于这个 ID 的消息
    /// </summary>
    [JsonPropertyName("around")]
    public string? Around { get; set; }

    /// <summary>
    /// 获取这个消息ID之后的消息
    /// </summary>
    [JsonPropertyName("before")]
    public string? Before { get; set; }

    /// <summary>
    /// 获取这个消息ID之前的消息
    /// </summary>
    [JsonPropertyName("after")]
    public string? After { get; set; }

    /// <summary>
    /// 获取多少条 1 - 100
    /// </summary>

    [JsonPropertyName("limit")]
    public int Limit { get; set; } = 50;
}
