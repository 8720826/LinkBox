using LinkBox.DTOs;
using LinkBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkBox.APIs.v1;

/// <summary>
/// 配置 API 控制器
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ConfigsController : ControllerBase
{
    private readonly IConfigService _configService;
    private readonly ILogger<ConfigsController> _logger;

    public ConfigsController(IConfigService configService, ILogger<ConfigsController> logger)
    {
        _configService = configService;
        _logger = logger;
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    [HttpGet("{name}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> GetConfig(string name)
    {
        var value = await _configService.GetConfigAsync(name);
        if (string.IsNullOrEmpty(value))
        {
            return NotFound();
        }
        return Ok(value);
    }

    /// <summary>
    /// 设置配置
    /// </summary>
    [HttpPut("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetConfig(string name, [FromBody] string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return BadRequest("值不能为空");
        }

        await _configService.SetConfigAsync(name, value);
        return NoContent();
    }

    /// <summary>
    /// 验证密码
    /// </summary>
    [HttpPost("verify-password")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> VerifyPassword([FromBody] string password)
    {
        var isValid = await _configService.VerifyPasswordAsync(password);
        return Ok(isValid);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    [HttpPost("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.OldPassword) || 
            string.IsNullOrWhiteSpace(request.NewPassword))
        {
            return BadRequest("密码不能为空");
        }

        try
        {
            await _configService.ChangePasswordAsync(request.OldPassword, request.NewPassword);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}

/// <summary>
/// 密码修改请求
/// </summary>
public record PasswordChangeRequest
{
    public string OldPassword { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
}
