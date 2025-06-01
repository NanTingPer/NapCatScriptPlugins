#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;

public class ThreadMetadata
{
    /// <summary>
    /// 线程是否被归档
    /// </summary>
    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    /// <summary>
    /// 线程在 inactive 分钟后自动从频道列表隐藏
    /// 可选值：60, 1440, 4320, 10080
    /// </summary>
    [JsonPropertyName("auto_archive_duration")]
    public int AutoArchiveDuration { get; set; }

    /// <summary>
    /// 线程归档状态最后变更的时间戳（用于计算最近活动）
    /// </summary>
    [JsonPropertyName("archive_timestamp")]
    public DateTimeOffset ArchiveTimestamp { get; set; }

    /// <summary>
    /// 线程是否被锁定（锁定后只有 MANAGE_THREADS 权限的用户可以取消归档）
    /// </summary>
    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// 非管理员是否可以将其他非管理员添加到线程（仅私有线程可用）
    /// </summary>
    [JsonPropertyName("invitable")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Invitable { get; set; }

    /// <summary>
    /// 线程创建时间戳（仅对2022-01-09之后创建的线程有效）
    /// </summary>
    [JsonPropertyName("create_timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? CreateTimestamp { get; set; }
}