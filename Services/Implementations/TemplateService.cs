using LinkBox.Common;
using LinkBox.Services.Interfaces;

namespace LinkBox.Services.Implementations;

/// <summary>
/// 模板服务实现
/// </summary>
public class TemplateService : ITemplateService
{
    private readonly string _templatePath;
    private readonly ILogger<TemplateService> _logger;

    public TemplateService(IHostEnvironment environment, ILogger<TemplateService> logger)
    {
        _templatePath = Path.Combine(environment.ContentRootPath, "wwwroot", "template");
        _logger = logger;
        
        // 确保模板目录存在
        if (!Directory.Exists(_templatePath))
        {
            Directory.CreateDirectory(_templatePath);
        }
    }

    public Task<string> GetHtmlTemplateAsync()
    {
        var path = Path.Combine(_templatePath, "index.tpl");
        return File.Exists(path) 
            ? Task.FromResult(File.ReadAllText(path)) 
            : Task.FromResult(string.Empty);
    }

    public Task<string> GetCssTemplateAsync()
    {
        var path = Path.Combine(_templatePath, "index.css");
        return File.Exists(path) 
            ? Task.FromResult(File.ReadAllText(path)) 
            : Task.FromResult(string.Empty);
    }

    public Task<string> GetJsTemplateAsync()
    {
        var path = Path.Combine(_templatePath, "index.js");
        return File.Exists(path) 
            ? Task.FromResult(File.ReadAllText(path)) 
            : Task.FromResult(string.Empty);
    }

    public async Task UpdateHtmlTemplateAsync(string html)
    {
        var path = Path.Combine(_templatePath, "index.tpl");
        await File.WriteAllTextAsync(path, html);
        _logger.LogInformation("更新 HTML 模板成功");
    }

    public async Task UpdateCssTemplateAsync(string css)
    {
        var path = Path.Combine(_templatePath, "index.css");
        await File.WriteAllTextAsync(path, css);
        _logger.LogInformation("更新 CSS 模板成功");
    }

    public async Task UpdateJsTemplateAsync(string js)
    {
        var path = Path.Combine(_templatePath, "index.js");
        await File.WriteAllTextAsync(path, js);
        _logger.LogInformation("更新 JS 模板成功");
    }

    public Task CompileTemplateAsync()
    {
        // 触发模板编译
        TemplateProvider.NextCompileTime = DateTime.Now;
        _logger.LogInformation("触发模板编译");
        return Task.CompletedTask;
    }
}
