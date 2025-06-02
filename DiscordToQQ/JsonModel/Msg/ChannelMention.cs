#pragma warning disable CS8618
using System.Text.Json.Serialization;
using DiscordToQQ.JsonModel.Channel;

namespace DiscordToQQ.JsonModel.Msg;

/// <summary>
/// 提及频道
/// </summary>
public class ChannelMention
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("guild_id")]
    public string GuildId { get; set; }

    /// <summary>
    /// <see cref="ChannelType"/>
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
