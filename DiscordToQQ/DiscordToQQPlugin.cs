#pragma warning disable CS8618
using DiscordToQQ.JsonModel.Msg;
using NapCatScript.Core;
using NapCatScript.Core.JsonFormat;
using NapCatScript.Core.JsonFormat.Msgs;
using NapCatScript.Core.Model;
using NapCatScript.Core.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordToQQ;

public class DiscordToQQPlugin : PluginType
{
    private const string AUTCONFIG = "DiscordAuthorization";
    private const string GROUPSCONFIG = "DiscordToGroups";
    private const string CURRMAXID = "DiscordCurrentId";
    public readonly static string? GetMsgAPI = "https://discord.com/api/v9/channels/";
    /// <summary>
    /// 要转发到哪些群组
    /// </summary>
    public readonly static List<string> GoToGroups = [];
    public static string? Authorization { get; set; }
    public override void Init()
    {
        //获取访问Aut密钥
        Authorization =  Config.GetConf(AUTCONFIG);
        string? groups = Config.GetConf(GROUPSCONFIG);
        string? maxid = Config.GetConf(CURRMAXID);
        if(string.IsNullOrEmpty(Authorization)) {
            Log.InstanceLog.Erro("DiscordAuthorization配置已生成，请设置!");
            throw new Exception();
        }

        if (string.IsNullOrEmpty(maxid)) {
            maxMsgId = "0";
            Config.SetConf(CURRMAXID, "0");
        }

        GoToGroups.AddRange(GetValues(groups));

        _ = Task.Run(MsgToQQ);
    }

    public override async Task Run(MsgInfo msg, string httpUri)
    {
        await Task.FromResult(1);
    }

    /// <summary>
    /// 存储50条历史消息
    /// </summary>
    private static List<DiscordMessage> oldMsgs = [];

    /// <summary>
    /// 已经转发的最大消息id
    /// </summary>
    private static string maxMsgId = "0";

    private bool init = false;
    private async Task MsgToQQ()
    {
        var getApi = "https://discord.com/api/v9/channels/369115362186231809/messages?limit=50";
        var htc = new HttpClient();
        htc.DefaultRequestHeaders.Add("Authorization", Authorization);

        while (true) {
            if (init == false) {
                init = true;
            } else {
                await Task.Delay(TimeSpan.FromMinutes(30));
            }
            var getRespMsg = await htc.GetAsync(getApi);
            var msgJson = await getRespMsg.Content.ReadAsStringAsync();
            if (msgJson is null)
                return;

            if (!msgJson.GetJsonElement(out var je))
                return;

            //遍历消息 并载入
            foreach (var item in je.EnumerateArray().AsEnumerable()) {
                try {
                    var obj = item.Deserialize<DiscordMessage>();
                    if (obj is null)
                        continue;

                    if (oldMsgs.Any(f => f.Id == obj.Id))
                        continue;

                    oldMsgs.Add(obj);
                } catch (Exception e) {
                    Log.InstanceLog.Erro("消息序列化失败！", e.Message, e.StackTrace);
                }
            }

            //按照时间排序
            oldMsgs = oldMsgs.OrderBy(dmsg => dmsg.Id).ToList();

            var count = oldMsgs.Count;
            if (count > 50) {
                oldMsgs = oldMsgs.TakeLast(50).ToList();
            }

            if (count <= 0)
                return;

            //转发最新消息
            var sendMsg = oldMsgs[^1];

            if (!ulong.TryParse(sendMsg.Id, out ulong newid))
                continue;

            if (!ulong.TryParse(maxMsgId, out ulong oldid))
                continue;

            if (!(newid > oldid))
                continue;

            Log.Info("Discord 有最新消息！/ Discord new message!");
            foreach (var groupId in GoToGroups) {
                Send.SendMsg(groupId, MsgTo.group, new TextJson(sendMsg.Content));
            }
            maxMsgId = sendMsg.Id;
            Config.SetConf(CURRMAXID, sendMsg.Id);
        }
    }

    private static string[] GetValues(string? str)
    {
        if (str == null)
            return [];
        var values = str.Split(",");
        for (int i = 0; i < values.Length; i++) {
            values[i] = values[i].Trim();
        }
        return values;
    }
}