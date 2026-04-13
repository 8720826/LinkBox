namespace LinkBox.Common;

/// <summary>
/// 应用常量定义
/// </summary>
public static class AppConstants
{
    /// <summary>
    /// 默认分页大小
    /// </summary>
    public const int DefaultPageSize = 10;

    /// <summary>
    /// 最大分页大小
    /// </summary>
    public const int MaxPageSize = 100;

    /// <summary>
    /// Cookie 过期天数
    /// </summary>
    public const int CookieExpirationDays = 30;

    /// <summary>
    /// 认证方案名称
    /// </summary>
    public const string AuthenticationScheme = "userAuth";

    /// <summary>
    /// 默认密码
    /// </summary>
    public const string DefaultPassword = "admin";

    /// <summary>
    /// 模板目录名
    /// </summary>
    public const string TemplateDirectoryName = "template";

    /// <summary>
    /// 图标目录名
    /// </summary>
    public const string IconDirectoryName = "icon";

    /// <summary>
    /// 数据目录名
    /// </summary>
    public const string DataDirectoryName = "data";

    /// <summary>
    /// 数据库文件名
    /// </summary>
    public const string DatabaseFileName = "linkbox.db";
}

/// <summary>
/// 错误消息常量
/// </summary>
public static class ErrorMessages
{
    public const string NotFound = "未找到记录";
    public const string Unauthorized = "未授权访问";
    public const string Forbidden = "禁止访问";
    public const string InvalidInput = "输入无效";
    public const string DuplicateRecord = "记录已存在";
    public const string DeleteFailed = "删除失败";
    public const string CreateFailed = "创建失败";
    public const string UpdateFailed = "更新失败";
}
