using LinkBox.Contexts;
using LinkBox.DTOs;
using LinkBox.Services.Implementations;
using LinkBox.Services.Interfaces;

namespace LinkBox.Services;

/// <summary>
/// 服务注册扩展方法
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加应用服务
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // 注册数据服务
        services.AddScoped<ILinkService, LinkService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IConfigService, ConfigService>();
        services.AddScoped<ITemplateService, TemplateService>();
        
        return services;
    }
}
