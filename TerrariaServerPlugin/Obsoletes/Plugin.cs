//using NapCatScript.Core;
//using NapCatScript.Core.JsonFormat.Msgs;
//using NapCatScript.Core.Model;
//using NapCatScript.Core.Services;
//using System.Text;
//using TerrariaServerSystem;

//namespace TerrariaServerPlugin.Obsoletes;

//[Obsolete("此类已过时")]
//public class Plugin : PluginType
//{
//    public readonly static ServerManager serverManager = new();
//    public readonly List<int> takeUpProts = [];//已经使用的
//    public readonly List<int> allPorts = [7777, 7778, 7779];//全部可以用的
//    public override void Init()
//    {
//        string? conf = Config.GetConf("TSPATH");
//        if(conf == null) {
//            Log.InstanceLog.Erro($"{nameof(TerrariaServerPlugin)}插件初始化时，未获取到配置: TSPATH");
//        } else {
//            Environment.SetEnvironmentVariable("TSPATH", conf);
//        }
//    }

//    public override async Task Run(MsgInfo msg, string httpUri)
//    {
//        await Task.CompletedTask;
//        var content = msg.MessageContent.Trim();
//        if (content.StartsWith("创建世界:")) {
//            await CreateWorld(msg);
//        } else if (content.StartsWith("创建服务器:")) {
//            await CreateServer(msg);
//        } else if (content.StartsWith("查看服务器列表:")) {
//            ViewServerList(msg);
//        } else if (content.StartsWith("查看服务器帮助")) {
//            ViewServerHelp(msg);
//        }
//    }

//    private void ViewServerHelp(MsgInfo msg)
//    {
//        string createWorldHelp = """
//            创建世界:世界大小,世界难度,邪恶,名称,种子
//            世界大小应该为1/2/3。1小,2中,3大
//            世界难度应该为1/2/3/4。1简,2专,3师,4旅
//            世界邪恶应该为1/2/3。1随,2腐,3猩红
//            """;

//        string createServerHelp = """
//            创建服务器:世界名称,密码,端口,服务器名称
//            其中密码可以不用，端口可以不用，服务器名称可以不用
//            可用端口仅: 7777, 7778, 7779
//            """;

//        Send.SendForawrd(msg,
//            [new TextJson(createWorldHelp),
//            new TextJson(createServerHelp)]
//            );
//    }

//    private void ViewServerList(MsgInfo msg)
//    {
//        List<TextJson> jsons = [];
//        var servers = serverManager.Servers;
//        for (int i = 0; i < serverManager.Servers.Count; i++) {
//            jsons.Add(new TextJson(servers[i]?.ToString() ?? ""));
//        }

//        Send.SendForawrd(msg, jsons);
//    }

//    private async Task CreateServer(MsgInfo msg)
//    {
//        //创建服务器:
//        if(serverManager.Servers.Count > 2) {
//            Send.SendMsg(msg, [new TextJson("当前存在服务器数量已达上限")]);
//        }
//        var content = msg.MessageContent.Replace('：',':').Replace('，',',');
//        var value = content["创建服务器:".Length..]
//            .Split(',')
//            .Select(s => s.Trim())
//            .ToArray();

//        //创建服务器:世界名称,密码,端口,服务器名称
//        var infos = new TerrariaServerInfo();
//        try {
//            infos = GetServerInfo(value, msg);
//        } catch (Exception e) {
//            StringBuilder sbuilder = new();
//            e.StackTrace?
//                .Split(Environment.NewLine)
//                .Where(f => f.Contains(nameof(TerrariaServerPlugin)))
//                .ToList()
//                .ForEach(f => sbuilder.AppendLine(f));
//            Send.SendMsg(msg, [new TextJson(e.Message + sbuilder.ToString())]);
//            return;
//        }

//        try {
//            await serverManager.AddServer(infos);
//            Send.SendMsg(msg, [new TextJson($"{msg.UserName} - 服务器创建成功！{infos.Port}")]);
//        } catch (Exception ex) {
//            Send.SendMsg(msg, [new TextJson($"{msg.UserName} - 服务器创建失败！ {ex.Message}")]);
//        }
//    }

//    private async Task CreateWorld(MsgInfo msg)
//    {
//        //世界大小，世界难度，邪恶，名称，种子
//        //创建世界:1,3,
//        var content = msg.MessageContent.Replace('：', ':');
//        object[]? parms = null;
//        try {
//            parms = content["创建世界:".Length..]
//            .Replace('，',',')
//            .Split(',')
//            .Select(s => s.Trim())
//            .ToArray();
//        } catch(Exception) {
//            Send.SendMsg(msg, [new TextJson("解析失败，是否未按要求输入！")]);
//            return;
//        }

//        WorldInfo world = new WorldInfo();
//        try {
//            world = GetWorldInfo(parms);
//        } catch(Exception e) {
//            Send.SendMsg(msg, [new TextJson(e.Message)]);
//            return;
//        }

//        await TerrariaServer.CreateWorld().CreateWorld(world);
//        Send.SendMsg(msg, [new TextJson($"{msg.UserName} 要求创建的世界已经完成了！")]);
//    }

//    private TerrariaServerInfo GetServerInfo(string[] parms, MsgInfo msg)
//    {
//        var serverInfo = new TerrariaServerInfo();
//        //世界名称,密码,端口,服务器名称
//        if(parms.Length <= 0) {
//            throw new Exception($"{msg.UserName} - 在创建服务器时，给没给参数");
//        }
        
//        if(parms.Length <= 1) {
//            serverInfo.WorldName = parms[0].ToString()!;
//        }

//        if(parms.Length <= 2) {
//            var ports = -1;
//            for (int i = 0; i < allPorts.Count; i++) {
//                int porta = allPorts[i];
//                if (!takeUpProts.Contains(porta)) {
//                    ports = porta;
//                    break;
//                }
//            }
//            if (ports == -1) {
//                throw new Exception("服务器端口已开满");
//            }
//            serverInfo.Port = ports;
//            takeUpProts.Add(ports);
//        }

//        if(parms.Length <= 3) {
//            serverInfo.Name = msg.UserName;
//        }

//        if(parms.Length == 4) {
//            var port = parms[2].ToString();
//            if (!int.TryParse(port, out var intPort)) {
//                throw new Exception($"{msg.UserName} - 在创建服务器时给了错误的端口");
//            }

//            //此端口不在允许的端口范围 或 此端口已经被使用
//            if (!allPorts.Contains(intPort) || takeUpProts.Contains(intPort)) {
//                throw new Exception($"{msg.UserName} - 在创建服务器时，使用了已经被占用的端口");
//            }
//            serverInfo.WorldName = parms[0].ToString() ?? "空的世界名称！";
//            serverInfo.Passwd = parms[1].ToString() ?? string.Empty;
//            serverInfo.Port = intPort;
//            serverInfo.Name = parms[3].ToString() ?? msg.UserName;
//        }
//        return serverInfo;
//    }

//    private WorldInfo GetWorldInfo(object[] parms)
//    {
//        var wi = new WorldInfo();
//        //大小 1, 2, 3
//        for (int i = 0; i < parms.Length; i++) {
//            object forValue = parms[i]!;
//            switch (i) {
//                case 0: //大小
//                {
//                    IfNotIntThrow("大小", forValue, out var value);
//                    if (value < 1 && value > 3) {
//                        throw new Exception("世界大小应该为1/2/3。1小,2中,3大");
//                    }
//                    wi.WorldSize = value;
//                    break;
//                } 
//                case 1: //难度
//                {
//                    IfNotIntThrow("难度", forValue, out var value);
//                    if (value < 1 && value > 4) {
//                        throw new Exception("世界难度应该为1/2/3/4。1简,2专,3师,4旅");
//                    }
//                    wi.WorldDifficulty = value;
//                    break;
//                }
//                case 2: //邪恶
//                {
//                    IfNotIntThrow("邪恶", forValue, out var value);
//                    if (value < 1 && value > 3) {
//                        throw new Exception("世界邪恶应该为1/2/3。1随,2腐,3猩红");
//                    }
//                    wi.WorldEvil = value;
//                    break;
//                }
//                case 3: //名称
//                    wi.WroldName = forValue.ToString() ?? "";
//                    break;
//                case 4: //种子
//                    wi.WroldSeed = forValue.ToString() ?? "";
//                    break;
//            }
//        }

//        return wi;
//    }

//    private void IfNotIntThrow(string valueName, object obj, out int value)
//    {
//        if(int.TryParse(obj.ToString(), out value)) {
//            return;
//        }
//        throw new Exception($"{valueName} : {obj.ToString()} 不是Int");
//    }
//}
