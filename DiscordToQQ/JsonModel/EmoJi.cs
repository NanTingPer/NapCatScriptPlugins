#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;

public class EmoJi
{
    [JsonPropertyName("emoji_id")]
    public string EmojiId { get; set; }

    [JsonPropertyName("emoji_name")]
    public string EmojiName { get; set; }
}
