using LinkBox.Common;
using LinkBox.Contexts;
using LinkBox.DTOs;
using LinkBox.Entities;
using LinkBox.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LinkBox.Services.Implementations;

/// <summary>
/// 分类服务实现
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly LinkboxDbContext _dbContext;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(LinkboxDbContext dbContext, ILogger<CategoryService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _dbContext.Categories
            .OrderBy(c => c.SortId)
            .ToListAsync();

        var categoryDtos = categories.Adapt<List<CategoryDto>>();
        
        // 填充每个分类的链接数量
        foreach (var category in categoryDtos)
        {
            category.LinkCount = await _dbContext.Links.CountAsync(l => l.CategoryId == category.Id);
        }

        return categoryDtos;
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null) return null;

        var dto = category.Adapt<CategoryDto>();
        dto.LinkCount = await _dbContext.Links.CountAsync(l => l.CategoryId == id);
        return dto;
    }

    public async Task<int> CreateCategoryAsync(CreateCategoryRequest request)
    {
        var category = request.Adapt<CategoryEntity>();
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("创建分类成功，ID: {CategoryId}", category.Id);
        return category.Id;
    }

    public async Task UpdateCategoryAsync(UpdateCategoryRequest request)
    {
        var category = await _dbContext.Categories.FindAsync(request.Id);
        if (category == null)
        {
            throw new KeyNotFoundException($"未找到 ID 为 {request.Id} 的分类");
        }

        category.Name = request.Name;
        category.SortId = request.SortId;
        category.Type = request.Type;

        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("更新分类成功，ID: {CategoryId}", request.Id);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"未找到 ID 为 {id} 的分类");
        }

        // 检查是否有链接使用该分类
        var linkCount = await _dbContext.Links.CountAsync(l => l.CategoryId == id);
        if (linkCount > 0)
        {
            throw new InvalidOperationException($"无法删除分类：该分类下还有 {linkCount} 个链接");
        }

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("删除分类成功，ID: {CategoryId}", id);
    }
}
