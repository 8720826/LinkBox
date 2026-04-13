using LinkBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkBox.APIs.v1;

/// <summary>
/// 模板 API 控制器
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateService _templateService;
    private readonly ILogger<TemplatesController> _logger;

    public TemplatesController(ITemplateService templateService, ILogger<TemplatesController> logger)
    {
        _templateService = templateService;
        _logger = logger;
    }

    /// <summary>
    /// 获取 HTML 模板
    /// </summary>
    [HttpGet("html")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> GetHtmlTemplate()
    {
        var html = await _templateService.GetHtmlTemplateAsync();
        return Ok(html);
    }

    /// <summary>
    /// 获取 CSS 模板
    /// </summary>
    [HttpGet("css")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> GetCssTemplate()
    {
        var css = await _templateService.GetCssTemplateAsync();
        return Ok(css);
    }

    /// <summary>
    /// 获取 JS 模板
    /// </summary>
    [HttpGet("js")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> GetJsTemplate()
    {
        var js = await _templateService.GetJsTemplateAsync();
        return Ok(js);
    }

    /// <summary>
    /// 更新 HTML 模板
    /// </summary>
    [HttpPut("html")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateHtmlTemplate([FromBody] string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return BadRequest("HTML 不能为空");
        }

        await _templateService.UpdateHtmlTemplateAsync(html);
        return NoContent();
    }

    /// <summary>
    /// 更新 CSS 模板
    /// </summary>
    [HttpPut("css")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCssTemplate([FromBody] string css)
    {
        if (string.IsNullOrWhiteSpace(css))
        {
            return BadRequest("CSS 不能为空");
        }

        await _templateService.UpdateCssTemplateAsync(css);
        return NoContent();
    }

    /// <summary>
    /// 更新 JS 模板
    /// </summary>
    [HttpPut("js")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateJsTemplate([FromBody] string js)
    {
        if (string.IsNullOrWhiteSpace(js))
        {
            return BadRequest("JS 不能为空");
        }

        await _templateService.UpdateJsTemplateAsync(js);
        return NoContent();
    }

    /// <summary>
    /// 编译模板
    /// </summary>
    [HttpPost("compile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CompileTemplate()
    {
        await _templateService.CompileTemplateAsync();
        return NoContent();
    }
}
