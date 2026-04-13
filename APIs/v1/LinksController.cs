using LinkBox.DTOs;
using LinkBox.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LinkBox.APIs.v1;

/// <summary>
/// 链接 API 控制器
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class LinksController : ControllerBase
{
    private readonly ILinkService _linkService;
    private readonly ILogger<LinksController> _logger;

    public LinksController(ILinkService linkService, ILogger<LinksController> logger)
    {
        _linkService = linkService;
        _logger = logger;
    }

    /// <summary>
    /// 获取所有链接
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<LinkDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LinkDto>>> GetAllLinks()
    {
        var links = await _linkService.GetAllLinksAsync();
        return Ok(links);
    }

    /// <summary>
    /// 分页查询链接
    /// </summary>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(PagedResult<LinkDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<LinkDto>>> GetPagedLinks([FromQuery] PagedQuery query)
    {
        var result = await _linkService.GetPagedLinksAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据 ID 获取链接
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LinkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LinkDto>> GetLinkById(int id)
    {
        var link = await _linkService.GetLinkByIdAsync(id);
        if (link == null)
        {
            return NotFound();
        }
        return Ok(link);
    }

    /// <summary>
    /// 创建链接
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> CreateLink([FromBody] CreateLinkRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var id = await _linkService.CreateLinkAsync(request);
            return CreatedAtAction(nameof(GetLinkById), new { id }, id);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新链接
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateLink(int id, [FromBody] UpdateLinkRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest("ID 不匹配");
        }

        try
        {
            await _linkService.UpdateLinkAsync(request);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// 删除链接
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLink(int id)
    {
        try
        {
            await _linkService.DeleteLinkAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// 检查链接可用性
    /// </summary>
    [HttpPost("{id:int}/check")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckLinkAvailability(int id)
    {
        try
        {
            await _linkService.CheckLinkAvailabilityAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
