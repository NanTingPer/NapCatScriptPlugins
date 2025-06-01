#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;
/// <summary>
/// 标签结构（用于论坛/媒体频道）
/// <para> AvailableTags </para>
/// </summary>
public class Tag
{
    /// <summary>
    /// 标签ID（Snowflake）
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 标签名称（0-20个字符）
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否仅MANAGE_THREADS权限成员可管理此标签
    /// </summary>
    [JsonPropertyName("moderated")]
    public bool IsModerated { get; set; }

    /// <summary>
    /// 自定义表情ID（与emoji_name互斥）
    /// </summary>
    [JsonPropertyName("emoji_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EmojiId { get; set; }

    /// <summary>
    /// Unicode表情字符（与emoji_id互斥）
    /// </summary>
    [JsonPropertyName("emoji_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EmojiName { get; set; }
}