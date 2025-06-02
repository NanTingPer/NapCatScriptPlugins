#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel.Msg;

/// <summary>
/// Discord 附件数据结构（用于消息创建/编辑请求）
/// <para> 注意：在消息创建/编辑请求中，只需要提供 id 字段 </para>
/// </summary>
public class Attachment
{
    /// <summary>
    /// 附件ID（Snowflake，在编辑请求中必须提供）
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 附件文件名
    /// </summary>
    [JsonPropertyName("filename")]
    public string Filename { get; set; } = string.Empty;

    /// <summary>
    /// 文件标题（可选）
    /// </summary>
    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    /// <summary>
    /// 文件描述（最大1024字符）
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [StringLength(1024, ErrorMessage = "Description cannot exceed 1024 characters.")]
    public string? Description { get; set; }

    /// <summary>
    /// 附件媒体类型（如 "image/png"）
    /// </summary>
    [JsonPropertyName("content_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContentType { get; set; }

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    /// 文件源URL
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 代理URL
    /// </summary>
    [JsonPropertyName("proxy_url")]
    public string ProxyUrl { get; set; } = string.Empty;

    /// <summary>
    /// 图片高度（如果是图片）
    /// </summary>
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Height { get; set; }

    /// <summary>
    /// 图片宽度（如果是图片）
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Width { get; set; }

    /// <summary>
    /// 是否为临时附件（会自动在一段时间后删除）
    /// 临时附件在消息存在期间保证可用
    /// </summary>
    [JsonPropertyName("ephemeral")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsEphemeral { get; set; }

    /// <summary>
    /// 音频文件时长（秒，当前用于语音消息）
    /// </summary>
    [JsonPropertyName("duration_secs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? DurationSeconds { get; set; }

    /// <summary>
    /// 波形图（Base64编码的采样波形，当前用于语音消息）
    /// </summary>
    [JsonPropertyName("waveform")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Waveform { get; set; }

    /// <summary>
    /// 附件标志位掩码
    /// </summary>
    [JsonPropertyName("flags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Flags { get; set; }
}
