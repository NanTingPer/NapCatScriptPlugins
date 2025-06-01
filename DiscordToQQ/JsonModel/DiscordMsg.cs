#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;

public class DiscordMsg
{
    /// <summary>
    /// 频道的 ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 频道类型
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }

    /// <summary>
    /// 所属公会的 ID（某些通过网关公会分发接收的频道对象可能缺少此字段）
    /// </summary>
    [JsonPropertyName("guild_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GuildId { get; set; }

    /// <summary>
    /// 频道的排序位置（相同位置的频道按 ID 排序）
    /// </summary>
    [JsonPropertyName("position")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Position { get; set; }

    /// <summary>
    /// 成员和角色的显式权限覆盖
    /// </summary>
    [JsonPropertyName("permission_overwrites")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<PermissionOverwrite>? PermissionOverwrites { get; set; }

    /// <summary>
    /// 频道名称（1-100 个字符）
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// 频道主题（GUILD_FORUM 和 GUILD_MEDIA 频道为 0-4096 字符，其他频道为 0-1024 字符）
    /// </summary>
    [JsonPropertyName("topic")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Topic { get; set; }

    /// <summary>
    /// 频道是否为 NSFW（不适合工作场所）内容
    /// </summary>
    [JsonPropertyName("nsfw")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Nsfw { get; set; }

    /// <summary>
    /// 此频道中最后发送的消息的 ID（对于 GUILD_FORUM 或 GUILD_MEDIA 频道则为线程 ID）
    /// （可能指向不存在的消息或线程）
    /// </summary>
    [JsonPropertyName("last_message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastMessageId { get; set; }

    /// <summary>
    /// 语音频道的比特率（以位为单位）
    /// </summary>
    [JsonPropertyName("bitrate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Bitrate { get; set; }

    /// <summary>
    /// 语音频道的用户限制
    /// </summary>
    [JsonPropertyName("user_limit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? UserLimit { get; set; }

    /// <summary>
    /// 用户发送下一条消息前需要等待的秒数（0-21600）
    /// 拥有 manage_messages 或 manage_channel 权限的机器人和用户不受影响
    /// </summary>
    [JsonPropertyName("rate_limit_per_user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? RateLimitPerUser { get; set; }

    /// <summary>
    /// 私聊的接收者
    /// </summary>
    [JsonPropertyName("recipients")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<User>? RecipientsUser { get; set; }

    /// <summary>
    /// 群组私聊的图标哈希
    /// </summary>
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }

    /// <summary>
    /// 群组私聊或线程的创建者 ID
    /// </summary>
    [JsonPropertyName("owner_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OwnerId { get; set; }

    /// <summary>
    /// 如果是机器人创建的群组私聊，则为创建者的应用 ID
    /// </summary>
    [JsonPropertyName("application_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ApplicationId { get; set; }

    /// <summary>
    /// 对于群组私聊频道：是否由应用程序通过 gdm.join OAuth2 范围管理
    /// </summary>
    [JsonPropertyName("managed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Managed { get; set; }

    /// <summary>
    /// 对于公会频道：频道父类别的 ID（每个父类别最多可包含 50 个频道）
    /// 对于线程：创建此线程的文本频道 ID
    /// </summary>
    [JsonPropertyName("parent_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ParentId { get; set; }

    /// <summary>
    /// 最后置顶消息的置顶时间（当消息未置顶时为 null）
    /// </summary>
    [JsonPropertyName("last_pin_timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? LastPinTimestamp { get; set; }

    /// <summary>
    /// 语音频道的语音区域 ID，设置为 null 时自动选择
    /// </summary>
    [JsonPropertyName("rtc_region")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RtcRegion { get; set; }

    /// <summary>
    /// 语音频道的摄像头视频质量模式，不存在时为 1
    /// </summary>
    [JsonPropertyName("video_quality_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? VideoQualityMode { get; set; }

    /// <summary>
    /// 线程中的消息数（不包括初始消息或已删除的消息）
    /// </summary>
    [JsonPropertyName("message_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MessageCount { get; set; }

    /// <summary>
    /// 线程中用户的近似计数，最多计数到 50
    /// </summary>
    [JsonPropertyName("member_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MemberCount { get; set; }

    /// <summary>
    /// 线程特定的元数据字段，其他频道不需要
    /// </summary>
    [JsonPropertyName("thread_metadata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ThreadMetadata? ThreadMetadata { get; set; }

    /// <summary>
    /// 当前用户的线程成员对象（如果已加入线程），仅包含在某些 API 端点中
    /// </summary>
    [JsonPropertyName("member")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ThreadMember? Member { get; set; }

    /// <summary>
    /// 新创建线程的默认自动归档持续时间（分钟）
    /// 线程在指定时间内不活动后将不再显示在频道列表中
    /// 可设置为：60, 1440, 4320, 10080
    /// </summary>
    [JsonPropertyName("default_auto_archive_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DefaultAutoArchiveDuration { get; set; }

    /// <summary>
    /// 频道权限位掩码
    /// </summary>
    [JsonPropertyName("permissions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Permissions { get; set; }

    /// <summary>
    /// 频道标志位掩码
    /// </summary>
    [JsonPropertyName("flags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Flags { get; set; }

    /// <summary>
    /// 线程中曾经发送的消息总数
    /// 类似于 message_count，但在消息删除时不会减少
    /// </summary>
    [JsonPropertyName("total_message_sent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalMessageSent { get; set; }

    /// <summary>
    /// 可在 GUILD_FORUM 或 GUILD_MEDIA 频道中使用的标签集合
    /// </summary>
    [JsonPropertyName("available_tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Tag>? AvailableTags { get; set; }

    /// <summary>
    /// 已应用于 GUILD_FORUM 或 GUILD_MEDIA 频道中线程的标签 ID 集合
    /// </summary>
    [JsonPropertyName("applied_tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? AppliedTags { get; set; }

    /// <summary>
    /// 在 GUILD_FORUM 或 GUILD_MEDIA 频道的线程中添加反应按钮上显示的默认表情
    /// </summary>
    [JsonPropertyName("default_reaction_emoji")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public EmoJi? DefaultReactionEmoji { get; set; }

    /// <summary>
    /// 频道中新创建线程的初始 rate_limit_per_user 值
    /// 此字段在创建时复制到线程，不会实时更新
    /// </summary>
    [JsonPropertyName("default_thread_rate_limit_per_user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DefaultThreadRateLimitPerUser { get; set; }

    /// <summary>
    /// 用于排序 GUILD_FORUM 和 GUILD_MEDIA 频道中帖子的默认排序类型
    /// 默认为 null，表示频道管理员尚未设置首选排序顺序
    /// </summary>
    [JsonPropertyName("default_sort_order")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DefaultSortOrder { get; set; }

    /// <summary>
    /// 用于显示 GUILD_FORUM 频道帖子的默认论坛布局视图
    /// 默认为 0，表示频道管理员尚未设置布局视图
    /// </summary>
    [JsonPropertyName("default_forum_layout")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DefaultForumLayout { get; set; }
}

