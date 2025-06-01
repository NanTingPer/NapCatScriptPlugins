#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;

public class GuildMember
{
    /// <summary>
    /// 成员关联的用户对象（在 MESSAGE_CREATE 和 MESSAGE_UPDATE 事件中不包含）
    /// </summary>
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; set; }

    /// <summary>
    /// 成员在公会的昵称（1-32字符）
    /// </summary>
    [JsonPropertyName("nick")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Nickname { get; set; }

    /// <summary>
    /// 成员的公会头像哈希
    /// </summary>
    [JsonPropertyName("avatar")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GuildAvatar { get; set; }

    /// <summary>
    /// 成员的公会横幅哈希
    /// </summary>
    [JsonPropertyName("banner")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GuildBanner { get; set; }

    /// <summary>
    /// 成员拥有的角色ID数组
    /// </summary>
    [JsonPropertyName("roles")]
    public List<string> RoleIds { get; set; } = new List<string>();

    /// <summary>
    /// 成员加入公会的时间戳
    /// </summary>
    [JsonPropertyName("joined_at")]
    public DateTimeOffset JoinedAt { get; set; }

    /// <summary>
    /// 成员开始服务器提升（Boost）的时间戳
    /// </summary>
    [JsonPropertyName("premium_since")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? PremiumSince { get; set; }

    /// <summary>
    /// 是否在语音频道被禁听
    /// </summary>
    [JsonPropertyName("deaf")]
    public bool IsDeafened { get; set; }

    /// <summary>
    /// 是否在语音频道被静音
    /// </summary>
    [JsonPropertyName("mute")]
    public bool IsMuted { get; set; }

    /// <summary>
    /// 成员标志位掩码（默认0）
    /// </summary>
    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    /// <summary>
    /// 是否未通过公会的会员筛查要求（仅在 GUILD_ 事件中包含）
    /// </summary>
    [JsonPropertyName("pending")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsPending { get; set; }

    /// <summary>
    /// 成员在频道中的总权限（包含覆盖权限，仅在交互对象中返回）
    /// </summary>
    [JsonPropertyName("permissions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Permissions { get; set; }

    /// <summary>
    /// 成员 timeout 过期时间（为 null 或过去时间表示未受限）
    /// </summary>
    [JsonPropertyName("communication_disabled_until")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? TimeoutUntil { get; set; }

    /// <summary>
    /// 成员公会头像装饰数据
    /// </summary>
    [JsonPropertyName("avatar_decoration_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserAvatarDecoration? AvatarDecorationData { get; set; }
}

