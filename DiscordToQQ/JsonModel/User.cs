#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;
public class User
{
    /// <summary>
    /// 用户ID（Snowflake）
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 用户名（不跨平台唯一）
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 用户Discord标签（例如：1234）
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("discriminator")]
    public string Discriminator { get; set; } = string.Empty;

    /// <summary>
    /// 用户显示名称（如果设置）。对于机器人，这是应用名称
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("global_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GlobalName { get; set; }

    /// <summary>
    /// 用户头像哈希
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("avatar")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Avatar { get; set; }

    /// <summary>
    /// 是否属于OAuth2应用（机器人）
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("bot")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Bot { get; set; }

    /// <summary>
    /// 是否是官方Discord系统用户（紧急消息系统的一部分）
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("system")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? System { get; set; }

    /// <summary>
    /// 是否启用了双重认证
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("mfa_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MfaEnabled { get; set; }

    /// <summary>
    /// 用户横幅哈希
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("banner")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Banner { get; set; }

    /// <summary>
    /// 用户横幅颜色（十六进制颜色代码的整数表示）
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("accent_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? AccentColor { get; set; }

    /// <summary>
    /// 用户选择的语言选项
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("locale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Locale { get; set; }

    /// <summary>
    /// 用户邮箱是否已验证
    /// Required OAuth2 Scope: email
    /// </summary>
    [JsonPropertyName("verified")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Verified { get; set; }

    /// <summary>
    /// 用户邮箱地址
    /// Required OAuth2 Scope: email
    /// </summary>
    [JsonPropertyName("email")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; set; }

    /// <summary>
    /// 用户账户标志位掩码
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("flags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Flags { get; set; }

    /// <summary>
    /// 用户Nitro订阅类型
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("premium_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PremiumType { get; set; }

    /// <summary>
    /// 用户公开标志位掩码
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("public_flags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PublicFlags { get; set; }

    /// <summary>
    /// 用户头像装饰数据
    /// Required OAuth2 Scope: identify
    /// </summary>
    [JsonPropertyName("avatar_decoration_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserAvatarDecoration? AvatarDecorationData { get; set; }
}

