using System.Threading.Tasks;

namespace TestConsole;

internal class Program
{
    static async Task Main(string[] args)
    {
        DiscordToQQTest.DiscordMsgParser();

        var pl = new TerrariaServerPlugin.Plugin();
        await pl.Run(new NapCatScript.Core.Model.MsgInfo() { MessageContent = "创建服务器:新世界" }, "");
    }
}
