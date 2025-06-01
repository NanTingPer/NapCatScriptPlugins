namespace DiscordToQQ.JsonModel;

public enum MsgType
{
    /// <summary>
    /// 服务器 普通文本
    /// </summary>
    GuildText = 0,

    /// <summary>
    /// 私聊 普通文本
    /// </summary>
    DM = 1,

    /// <summary>
    /// 服务器 语音
    /// </summary>
    GuildVoice = 2,

    /// <summary>
    /// 群组 直接消息
    /// </summary>
    GroupDM = 3,

    /// <summary>
    /// 渠道组织类别
    /// </summary>
    GuildCategory = 4,

    /// <summary>
    /// 用户可以关注并交叉发布到自己服务器的频道
    /// <para> 以前是 新闻 </para>
    /// </summary>
    GuildAnnouncement = 5,

    /// <summary>
    /// 新闻中的临时子频道
    /// </summary>
    AnnouncementThread = 10,

    /// <summary>
    /// <see cref="GuildText"/> 或者 <see cref="GuildForum"/> 频道中的子频道
    /// </summary>
    PublicThread = 11,

    /// <summary>
    /// <see cref="GuildText"/>频道中的临时子频道，只有受到邀请和拥有 MANAGE_THREADS权限的人才能查看
    /// </summary>
    PrivateThread = 12,

    /// <summary>
    /// 用于举办活动的语音频道
    /// </summary>
    GuildStageVoice = 13,

    /// <summary>
    /// 包含目录中的频道
    /// </summary>
    GuildDirectory = 14,

    GuildForum = 15,

    /// <summary>
    /// GUILD_FORUM
    /// </summary>
    GuildMedia = 16
}
