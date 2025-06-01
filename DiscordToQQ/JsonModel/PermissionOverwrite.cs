#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;

/// <summary>
/// 权限覆盖
/// </summary>
public class PermissionOverwrite
{
    /// <summary>
    /// 角色或用户的 ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 覆盖类型：0 表示角色，1 表示成员
    /// </summary>
    [JsonPropertyName("type")]
    public OverwriteType Type { get; set; }

    /// <summary>
    /// 允许的权限位集合（字符串形式的权限位掩码）
    /// </summary>
    [JsonPropertyName("allow")]
    public string Allow { get; set; } = "0";

    /// <summary>
    /// 拒绝的权限位集合（字符串形式的权限位掩码）
    /// </summary>
    [JsonPropertyName("deny")]
    public string Deny { get; set; } = "0";
}
public enum OverwriteType
{
    /// <summary>
    /// 角色覆盖 (0)
    /// </summary>
    Role = 0,

    /// <summary>
    /// 成员覆盖 (1)
    /// </summary>
    Member = 1
}
