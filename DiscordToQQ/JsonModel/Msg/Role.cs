#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel.Msg;

/// <summary>
/// Discord 角色数据结构
/// </summary>
public class Role
{
    /// <summary>
    /// 角色ID（Snowflake）
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 角色名称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 角色颜色（十六进制颜色代码的整数表示）
    /// 示例：0xRRGGBB
    /// </summary>
    [JsonPropertyName("color")]
    public int Color { get; set; }

    /// <summary>
    /// 是否在用户列表中置顶显示该角色成员
    /// </summary>
    [JsonPropertyName("hoist")]
    public bool IsHoisted { get; set; }

    /// <summary>
    /// 角色图标哈希
    /// </summary>
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }

    /// <summary>
    /// 角色Unicode表情符号
    /// </summary>
    [JsonPropertyName("unicode_emoji")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UnicodeEmoji { get; set; }

    /// <summary>
    /// 角色位置（相同位置的角色按ID排序）
    /// </summary>
    [JsonPropertyName("position")]
    public int Position { get; set; }

    /// <summary>
    /// 权限位集合（字符串形式）
    /// </summary>
    [JsonPropertyName("permissions")]
    public string Permissions { get; set; } = "0";

    /// <summary>
    /// 是否由集成应用管理
    /// </summary>
    [JsonPropertyName("managed")]
    public bool IsManaged { get; set; }

    /// <summary>
    /// 是否可被@提及
    /// </summary>
    [JsonPropertyName("mentionable")]
    public bool IsMentionable { get; set; }

    /// <summary>
    /// 角色标签数据
    /// </summary>
    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RoleTags? Tags { get; set; }

    /// <summary>
    /// 角色标志位掩码
    /// </summary>
    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    // ========== 辅助方法 ========== //

    /// <summary>
    /// 获取颜色值的十六进制字符串表示（带#前缀）
    /// </summary>
    public string GetColorHex() => $"#{Color:X6}";

    /// <summary>
    /// 获取权限位数值
    /// </summary>
    public ulong GetPermissionBits() => ulong.Parse(Permissions);

    /// <summary>
    /// 获取角色图标URL（如有）
    /// </summary>
    public string? GetIconUrl(int size = 128)
    {
        return Icon != null
            ? $"https://cdn.discordapp.com/role-icons/{Id}/{Icon}.png?size={size}"
            : null;
    }
}

/// <summary>
/// Discord 角色标签数据结构
/// <para> 注意：类型为 null 的标签代表布尔值，存在且为 null 表示 true，不存在表示 false </para>
/// </summary>
public class RoleTags
{
    /// <summary>
    /// 此角色所属的机器人ID（如果为机器人专属角色）
    /// </summary>
    [JsonPropertyName("bot_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BotId { get; set; }

    /// <summary>
    /// 此角色所属的集成ID（如果为集成专属角色）
    /// </summary>
    [JsonPropertyName("integration_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IntegrationId { get; set; }

    /// <summary>
    /// 是否为服务器的Booster角色（Nitro提升角色）
    /// 特殊：当值为 null 时表示 true，字段不存在表示 false
    /// </summary>
    [JsonPropertyName("premium_subscriber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  // 必须使用 WhenWritingDefault
    public bool PremiumSubscriber { get; set; } = false;

    /// <summary>
    /// 此角色关联的订阅商品列表ID
    /// </summary>
    [JsonPropertyName("subscription_listing_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SubscriptionListingId { get; set; }

    /// <summary>
    /// 此角色是否可供购买
    /// 特殊：当值为 null 时表示 true，字段不存在表示 false
    /// </summary>
    [JsonPropertyName("available_for_purchase")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool AvailableForPurchase { get; set; } = false;

    /// <summary>
    /// 此角色是否为服务器的关联角色（Linked Role）
    /// 特殊：当值为 null 时表示 true，字段不存在表示 false
    /// </summary>
    [JsonPropertyName("guild_connections")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool GuildConnections { get; set; } = false;
}

