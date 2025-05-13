﻿namespace TestPlugin;

public class FAQ
{
    public static FAQ FAQI { get; set; } = new FAQ();
    public const string SplitChars = "FAQ#";
    public const string SplitChars2 = "###";

    /// <summary>
    /// 添加FAQ
    /// </summary>
    /// <param name="mesg"> 消息引用 </param>
    /// <param name="httpURI"> 请求URI(例 http://127.0.0.1:6666) 不含API </param>
    /// <param name="content"> 消息内容 </param>
    /// <returns></returns>
    public async void AddAsync(MsgInfo mesg, string httpURI, string content, CancellationToken ct)
    {
        //如果要设置权限的话，可以在这里进行
        string[] cons = content.Split(SplitChars)[1].Split(SplitChars2);
        if(cons.Length < 2) {
            SendTextAsync(mesg, httpURI, "是不是少了点什么?", ct);
            return;
        }
        var faq = new FAQModel() { Key = cons[0], Value = cons[1], CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"), UserId = mesg.UserId, UserName = mesg.UserName };
        try {
            await SQLService.Insert(faq);
        } catch (Exception e) {
            Log.Erro($"FAQ插入错误: {e.Message}\r\n {e.StackTrace}");
            return;
        }
        SendTextAsync(mesg, httpURI, "成功啦，试试？", ct);
    }

    /// <summary>
    /// 使用内容，从数据库获取此FAQ的答案，如果没有就返回null
    /// </summary>
    public async Task<FAQModel?> Get(string content)
    {
        FAQModel? co;
        try {
            co = await SQLService.Get<FAQModel>(content);
        } catch { return null; }

        return co;
    }

    /// <summary>
    /// 删除给定内容的FAQ
    /// </summary>
    public async void DeleteAsync(string content)
    {
        await SQLService.Delete<FAQModel>(content.Split(SplitChars)[1]);
    }

    public async Task<string> GetALL()
    {
        List<FAQModel> list = await SQLService.GetAll<FAQModel>();
        StringBuilder strb = new();
        foreach (var item in list) {
            strb.Append("问题: ");
            strb.Append(item.Key);
            strb.AppendLine();
            strb.Append("回答: ");
            strb.Append(item.Value);
            strb.AppendLine();
            strb.Append("创建人: ");
            strb.Append(item.UserName);
            strb.AppendLine();
            strb.AppendLine();
        }
        return strb.ToString();
    }
}
