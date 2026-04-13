using LinkBox.DTOs;

namespace LinkBox.Services.Interfaces;

/// <summary>
/// 链接服务接口
/// </summary>
public interface ILinkService
{
    /// <summary>
    /// 获取所有链接
    /// </summary>
    Task<List<LinkDto>> GetAllLinksAsync();

    /// <summary>
    /// 分页查询链接
    /// </summary>
    Task<PagedResult<LinkDto>> GetPagedLinksAsync(PagedQuery query);

    /// <summary>
    /// 根据 ID 获取链接
    /// </summary>
    Task<LinkDto?> GetLinkByIdAsync(int id);

    /// <summary>
    /// 创建链接
    /// </summary>
    Task<int> CreateLinkAsync(CreateLinkRequest request);

    /// <summary>
    /// 更新链接
    /// </summary>
    Task UpdateLinkAsync(UpdateLinkRequest request);

    /// <summary>
    /// 删除链接
    /// </summary>
    Task DeleteLinkAsync(int id);

    /// <summary>
    /// 检查链接可用性
    /// </summary>
    Task CheckLinkAvailabilityAsync(int id);
}

/// <summary>
/// 分类服务接口
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// 获取所有分类
    /// </summary>
    Task<List<CategoryDto>> GetAllCategoriesAsync();

    /// <summary>
    /// 根据 ID 获取分类
    /// </summary>
    Task<CategoryDto?> GetCategoryByIdAsync(int id);

    /// <summary>
    /// 创建分类
    /// </summary>
    Task<int> CreateCategoryAsync(CreateCategoryRequest request);

    /// <summary>
    /// 更新分类
    /// </summary>
    Task UpdateCategoryAsync(UpdateCategoryRequest request);

    /// <summary>
    /// 删除分类
    /// </summary>
    Task DeleteCategoryAsync(int id);
}

/// <summary>
/// 配置服务接口
/// </summary>
public interface IConfigService
{
    /// <summary>
    /// 获取配置
    /// </summary>
    Task<string> GetConfigAsync(string name);

    /// <summary>
    /// 设置配置
    /// </summary>
    Task SetConfigAsync(string name, string value);

    /// <summary>
    /// 验证密码
    /// </summary>
    Task<bool> VerifyPasswordAsync(string password);

    /// <summary>
    /// 修改密码
    /// </summary>
    Task ChangePasswordAsync(string oldPassword, string newPassword);
}

/// <summary>
/// 模板服务接口
/// </summary>
public interface ITemplateService
{
    /// <summary>
    /// 获取模板 HTML
    /// </summary>
    Task<string> GetHtmlTemplateAsync();

    /// <summary>
    /// 获取模板 CSS
    /// </summary>
    Task<string> GetCssTemplateAsync();

    /// <summary>
    /// 获取模板 JS
    /// </summary>
    Task<string> GetJsTemplateAsync();

    /// <summary>
    /// 更新模板 HTML
    /// </summary>
    Task UpdateHtmlTemplateAsync(string html);

    /// <summary>
    /// 更新模板 CSS
    /// </summary>
    Task UpdateCssTemplateAsync(string css);

    /// <summary>
    /// 更新模板 JS
    /// </summary>
    Task UpdateJsTemplateAsync(string js);

    /// <summary>
    /// 编译模板
    /// </summary>
    Task CompileTemplateAsync();
}
