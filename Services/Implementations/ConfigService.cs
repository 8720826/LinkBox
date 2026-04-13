using System.Security.Cryptography;
using LinkBox.Contexts;
using LinkBox.DTOs;
using LinkBox.Entities;
using LinkBox.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkBox.Services.Implementations;

/// <summary>
/// 配置服务实现
/// </summary>
public class ConfigService : IConfigService
{
    private readonly LinkboxDbContext _dbContext;
    private readonly ILogger<ConfigService> _logger;
    private readonly string _adminPassword;

    public ConfigService(LinkboxDbContext dbContext, ILogger<ConfigService> logger, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _logger = logger;
        _adminPassword = configuration["PASSWORD"] ?? "admin";
    }

    public async Task<string> GetConfigAsync(string name)
    {
        var config = await _dbContext.Configs.FirstOrDefaultAsync(c => c.Name == name);
        return config?.Title ?? string.Empty;
    }

    public async Task SetConfigAsync(string name, string value)
    {
        var config = await _dbContext.Configs.FirstOrDefaultAsync(c => c.Name == name);
        
        if (config == null)
        {
            config = new ConfigEntity
            {
                Name = name,
                Title = value
            };
            _dbContext.Configs.Add(config);
        }
        else
        {
            config.Title = value;
        }

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("更新配置成功，Name: {ConfigName}", name);
    }

    public Task<bool> VerifyPasswordAsync(string password)
    {
        // 简单密码验证，实际应该使用哈希比较
        return Task.FromResult(password == _adminPassword);
    }

    public async Task ChangePasswordAsync(string oldPassword, string newPassword)
    {
        if (!await VerifyPasswordAsync(oldPassword))
        {
            throw new UnauthorizedAccessException("原密码错误");
        }

        var config = await _dbContext.Configs.FirstOrDefaultAsync(c => c.Name == "Password");
        
        if (config == null)
        {
            config = new ConfigEntity
            {
                Name = "Password",
                Password = HashPassword(newPassword)
            };
            _dbContext.Configs.Add(config);
        }
        else
        {
            config.Password = HashPassword(newPassword);
        }

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("修改密码成功");
    }

    /// <summary>
    /// 哈希密码
    /// </summary>
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 验证哈希密码
    /// </summary>
    public static bool VerifyHashedPassword(string hashedPassword, string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        var hash = Convert.ToBase64String(bytes);
        return hashedPassword == hash;
    }
}
