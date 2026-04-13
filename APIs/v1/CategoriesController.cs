using LinkBox.DTOs;
using LinkBox.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LinkBox.APIs.v1;

/// <summary>
/// 分类 API 控制器
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// 获取所有分类
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    /// <summary>
    /// 根据 ID 获取分类
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    /// <summary>
    /// 创建分类
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var id = await _categoryService.CreateCategoryAsync(request);
            return CreatedAtAction(nameof(GetCategoryById), new { id }, id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新分类
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest("ID 不匹配");
        }

        try
        {
            await _categoryService.UpdateCategoryAsync(request);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// 删除分类
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
