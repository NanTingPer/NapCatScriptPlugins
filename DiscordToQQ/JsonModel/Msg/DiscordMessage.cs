#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel.Msg;

/// <summary>
/// https://discord.com/developers/docs/resources/message
/// </summary>
public class DiscordMessage
{
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// 频道ID
    /// </summary>
    [JsonPropertyName("channel_id")]
    public string ChannelId { get; set; }

    /// <summary>
    /// 消息作者（可能不是有效用户）
    /// </summary>
    [JsonPropertyName("author")]
    public User Author { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }

    /// <summary>
    /// 消息发送时间
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// 消息最后编辑时间（未编辑则为null）
    /// </summary>
    [JsonPropertyName("edited_timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? EditedTimestamp { get; set; }

    /// <summary>
    /// 是否为TTS（文本转语音）消息
    /// </summary>
    [JsonPropertyName("tts")]
    public bool IsTTS { get; set; }

    /// <summary>
    /// 是否@提及所有人
    /// </summary>
    [JsonPropertyName("mention_everyone")]
    public bool MentionsEveryone { get; set; }

    /// <summary>
    /// 被@提及的用户列表
    /// </summary>
    [JsonPropertyName("mentions")]
    public List<User> MentionedUsers { get; set; }

    /// <summary>
    /// 被@提及的角色ID列表
    /// </summary>
    [JsonPropertyName("mention_roles")]
    public List<Role> MentionedRoleIds { get; set; }

    /// <summary>
    /// 被@提及的频道列表
    /// </summary>
    [JsonPropertyName("mention_channels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ChannelMention>? MentionedChannels { get; set; }

    /// <summary>
    /// 消息附件列表
    /// </summary>
    [JsonPropertyName("attachments")]
    public List<Attachment> Attachments { get; set; }

    /// <summary>
    /// 消息嵌入内容列表
    /// </summary>
    [JsonPropertyName("embeds")]
    public List<object> Embeds { get; set; }

    /// <summary>
    /// 消息反应列表
    /// </summary>
    [JsonPropertyName("reactions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Reactions { get; set; }

    /// <summary>
    /// 消息随机数（用于验证消息发送）
    /// </summary>
    [JsonPropertyName("nonce")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Nonce { get; set; }

    /// <summary>
    /// 是否已置顶
    /// </summary>
    [JsonPropertyName("pinned")]
    public bool IsPinned { get; set; }

    /// <summary>
    /// 如果是Webhook生成的消息，此为Webhook ID
    /// </summary>
    [JsonPropertyName("webhook_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? WebhookId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }

    /// <summary>
    /// 消息活动数据（与Rich Presence相关）
    /// </summary>
    [JsonPropertyName("activity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Activity { get; set; }

    /// <summary>
    /// 消息应用数据（与Rich Presence相关）
    /// </summary>
    [JsonPropertyName("application")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Application { get; set; }

    /// <summary>
    /// 如果是交互消息或应用Webhook，此为应用ID
    /// </summary>
    [JsonPropertyName("application_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ApplicationId { get; set; }

    /// <summary>
    /// 消息标志位掩码
    /// </summary>
    [JsonPropertyName("flags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Flags { get; set; }

    /// <summary>
    /// 消息引用数据（用于跨帖子、频道关注、回复等）
    /// </summary>
    [JsonPropertyName("message_reference")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? MessageReference { get; set; }

    /// <summary>
    /// 被引用的消息（可能不完整）
    /// </summary>
    [JsonPropertyName("referenced_message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DiscordMessage? ReferencedMessage { get; set; }

    /// <summary>
    /// 交互元数据（如果是交互结果消息）
    /// </summary>
    [JsonPropertyName("interaction_metadata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? InteractionMetadata { get; set; }

    /// <summary>
    /// 交互数据（已弃用，改用interaction_metadata）
    /// </summary>
    [JsonPropertyName("interaction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Interaction { get; set; }

    /// <summary>
    /// 由此消息创建的线程（包含线程成员对象）
    /// </summary>
    [JsonPropertyName("thread")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Thread { get; set; }

    /// <summary>
    /// 消息组件（按钮、操作行等）
    /// </summary>
    [JsonPropertyName("components")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Components { get; set; }

    /// <summary>
    /// 消息贴纸项列表
    /// </summary>
    [JsonPropertyName("sticker_items")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? StickerItems { get; set; }

    /// <summary>
    /// 消息贴纸列表（已弃用）
    /// </summary>
    [JsonPropertyName("stickers")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Stickers { get; set; }

    /// <summary>
    /// 消息在线程中的近似位置（可能有间隔或重复）
    /// </summary>
    [JsonPropertyName("position")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Position { get; set; }

    /// <summary>
    /// 角色订阅数据（如果是ROLE_SUBSCRIPTION_PURCHASE消息）
    /// </summary>
    [JsonPropertyName("role_subscription_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? RoleSubscriptionData { get; set; }

    /// <summary>
    /// 解析数据（用于自动填充选择菜单中的用户、成员、频道和角色）
    /// </summary>
    [JsonPropertyName("resolved")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? ResolvedData { get; set; }

    /// <summary>
    /// 投票数据
    /// </summary>
    [JsonPropertyName("poll")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Poll { get; set; }

    /// <summary>
    /// 与消息关联的呼叫数据
    /// </summary>
    [JsonPropertyName("call")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Call { get; set; }
}
