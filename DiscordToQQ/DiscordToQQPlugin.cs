#pragma warning disable CS8618
using NapCatScript.Core;
using NapCatScript.Core.Model;
using NapCatScript.Core.Services;
using System.Text.Json.Serialization;

namespace DiscordToQQ;

public class DiscordToQQPlugin : PluginType
{
    private const string AutConfig = "DiscordAuthorization";
    public readonly static string? GetMsgAPI = "https://discord.com/api/v9/channels/";
    public static string? Authorization { get; set; }
    public override void Init()
    {
        //获取访问Aut密钥
        Authorization =  Config.GetConf(AutConfig);
        if(Authorization is null) {
            Console.WriteLine("DiscordAuthorization配置已生成，请设置!");
            throw new Exception();
        }
    }

    public override async Task Run(MsgInfo msg, string httpUri)
    {
        await Task.FromResult(1);
    }
}

public class DiscordMsgInfo
{

}