#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace DiscordToQQ.JsonModel;
/// <summary>
/// 头像额外信息
/// </summary>
public class UserAvatarDecoration
{
    [JsonPropertyName("asset")]
    public string Asset { get; set; }

    [JsonPropertyName("sku_id")]
    public string SkuId { get; set; }
}


