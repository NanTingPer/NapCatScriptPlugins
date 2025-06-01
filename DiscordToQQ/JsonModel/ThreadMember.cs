#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;

public class ThreadMember
{
    /// <summary>
    /// 线程ID（标注*表示可选字段，但实际在某些端点可能必填）
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ThreadId { get; set; }

    /// <summary>
    /// 用户ID（标注*表示可选字段，但实际在某些端点可能必填）
    /// </summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UserId { get; set; }

    /// <summary>
    /// 用户最后加入线程的时间戳（ISO8601格式）
    /// </summary>
    [JsonPropertyName("join_timestamp")]
    public DateTimeOffset JoinTimestamp { get; set; }

    /// <summary>
    /// 用户线程设置标志位（当前仅用于通知设置）
    /// </summary>
    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    /// <summary>
    /// 附加的用户公会成员信息（标注**表示仅在某些端点返回）
    /// </summary>
    [JsonPropertyName("member")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GuildMember? Member { get; set; }
}