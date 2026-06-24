using NapCatScript.Core;
using NapCatScript.Core.Services;

namespace CalamityRAGChatPlugin;

/// <summary>
/// RAG Chat 插件配置类
/// 所有配置 Key 均以 CRC 开头
/// </summary>
public static class CRCConfig
{
    // 配置 Key 常量定义（全部以 CRC 开头）
    /// <summary>
    /// 向量库uri
    /// </summary>
    public const string CRCVectorStoreUri = nameof(CRCVectorStoreUri);
    /// <summary>
    /// 嵌入模型uri
    /// </summary>
    public const string CRCEmbeddingUri = nameof(CRCEmbeddingUri);
    /// <summary>
    /// 管理员用户列表
    /// </summary>
    public const string CRCAdminUsers = nameof(CRCAdminUsers);
    /// <summary>
    /// AI提示词
    /// </summary>
    public const string CRCAIPromp = nameof(CRCAIPromp);
    /// <summary>
    /// 模型名称
    /// </summary>
    public const string CRCAIModel = nameof(CRCAIModel);
    /// <summary>
    /// 触发的开始字符串
    /// </summary>
    public const string CRCStartString = nameof(CRCStartString);
    public const string CRCAIEndpoint = nameof(CRCAIEndpoint);

    // 默认值
    private const string DEFAULT_PROMPT = @"请用纯文本回答，严禁使用任何Markdown格式符号（如加粗、斜体等），内容中绝不能出现星号包裹的文字。根据用户输入解决问题，回答需简短。如果用户的问题与游戏无关，直接拒绝回答。";

    private const string DEFAULT_START_STRING = "!对话";

    // 内存缓存
    private static string _cachedVectorStoreUri = null!;
    private static string _cachedEmbeddingUri = null!;
    private static List<string> _cachedAdminUsers = null!;
    private static string _cachedAIPromp = null!;
    private static string _cachedStartString = null!;
    private static bool _isInitialized = false;
    private static string _aiEndpoint = null!;
    private static string _cacheModelName = null!;

    /// <summary>
    /// 初始化配置缓存（调用一次即可）
    /// </summary>
    public static void Init()
    {
        if (_isInitialized) return;

        _cachedVectorStoreUri = GetConf(CRCVectorStoreUri) ?? "";
        _cachedEmbeddingUri = GetConf(CRCEmbeddingUri) ?? "";
        _cachedAdminUsers = GetAdminUsers();
        _cachedAIPromp = GetConf(CRCAIPromp) ?? DEFAULT_PROMPT;
        _cachedStartString = GetConf(CRCStartString) ?? DEFAULT_START_STRING;
        _cacheModelName = GetConf(CRCAIModel) ?? "deepseek-v4-flash";
        _aiEndpoint = GetConf(CRCAIEndpoint) ?? "https://api.deepseek.com";
        _isInitialized = true;
    }

    /// <summary>
    /// 刷新配置缓存
    /// </summary>
    public static void Refresh()
    {
        _isInitialized = false;
        Init();
    }

    /// <summary>
    /// 向量库 URI 字符串
    /// </summary>
    public static string VectorStoreUri
    {
        get
        {
            if (!_isInitialized) Init();
            return _cachedVectorStoreUri;
        }
    }

    public static string AIEndpoint
    {
        get
        {
            if (!_isInitialized) Init();
            return _aiEndpoint;
        }
    }

    /// <summary>
    /// Embedding URI 字符串
    /// </summary>
    public static string EmbeddingUri
    {
        get
        {
            if (!_isInitialized) Init();
            return _cachedEmbeddingUri;
        }
    }

    /// <summary>
    /// 管理员用户列表（QQ号）
    /// </summary>
    public static List<string> AdminUsers
    {
        get
        {
            if (!_isInitialized) Init();
            return _cachedAdminUsers;
        }
    }

    /// <summary>
    /// 系统提示词
    /// </summary>
    public static string Prompt
    {
        get
        {
            if (!_isInitialized) Init();
            return _cachedAIPromp;
        }
    }

    /// <summary>
    /// 会话启动字符串
    /// </summary>
    public static string StartString
    {
        get
        {
            if (!_isInitialized) Init();
            return _cachedStartString;
        }
    }

    public static string ModelName
    {
        get
        {
            if (!_isInitialized) Init();
            return _cacheModelName;
        }
    }

    /// <summary>
    /// 检查用户是否是管理员
    /// </summary>
    public static bool IsAdmin(string qqNumber)
    {
        if (!_isInitialized) Init();
        return _cachedAdminUsers.Contains(qqNumber);
    }

    /// <summary>
    /// 获取管理员用户列表
    /// </summary>
    private static List<string> GetAdminUsers()
    {
        string adminUsersStr = GetConf(CRCAdminUsers) ?? "";
        if (string.IsNullOrWhiteSpace(adminUsersStr))
        {
            return new List<string>();
        }
        return adminUsersStr.Split(new[] { ',', '，', ';', '；' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(s => s.Trim())
                           .Where(s => !string.IsNullOrEmpty(s))
                           .ToList();
    }

    /// <summary>
    /// 设置配置值
    /// </summary>
    public static bool SetConf(string key, string value)
    {
        try {
            SetConfValue(key, value);
            Refresh();
            return true;
        } catch {
            return false;
        }
    }

    /// <summary>
    /// 获取配置值（包装 Config.GetConf）
    /// </summary>
    private static string GetConf(string key)
    {
        return Config.GetConf(key)!;
    }

    /// <summary>
    /// 设置配置值（包装）
    /// </summary>
    private static void SetConfValue(string key, string value)
    {
        // 调用 NapCatScript 的配置保存方法
        Config.SetConf(key, value);
    }
}
