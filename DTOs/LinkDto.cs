using LinkBox.Entities;
using LinkBox.Entities.Enums;

namespace LinkBox.DTOs;

/// <summary>
/// 链接数据传输对象
/// </summary>
public record LinkDto
{
    public int Id { get; init; }
    public int CategoryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public int SortId { get; init; }
    public bool IsAvailable { get; init; }
    public DateTime LastCheckTime { get; init; }
    public DateTime LastAvailableTime { get; init; }
    public string? CategoryName { get; init; }
}

/// <summary>
/// 创建链接请求
/// </summary>
public record CreateLinkRequest
{
    public int CategoryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public int SortId { get; init; }
    public bool IsFetchIconFromLink { get; init; }
    public bool IsFetchTitleFromLink { get; init; }
    public bool IsFetchDescriptionFromLink { get; init; }
    public bool IsSaveIconToBase64 { get; init; }
    public bool IsSaveIconToLocal { get; init; }
    public bool IsCompileImmediately { get; init; }
}

/// <summary>
/// 更新链接请求
/// </summary>
public record UpdateLinkRequest
{
    public int Id { get; init; }
    public int CategoryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public int SortId { get; init; }
}

/// <summary>
/// 分类数据传输对象
/// </summary>
public record CategoryDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int SortId { get; init; }
    public CategoryTypeEnum Type { get; init; }
    public int LinkCount { get; init; }
}

/// <summary>
/// 创建分类请求
/// </summary>
public record CreateCategoryRequest
{
    public string Name { get; init; } = string.Empty;
    public int SortId { get; init; }
    public CategoryTypeEnum Type { get; init; }
}

/// <summary>
/// 更新分类请求
/// </summary>
public record UpdateCategoryRequest
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int SortId { get; init; }
    public CategoryTypeEnum Type { get; init; }
}

/// <summary>
/// 分页查询参数
/// </summary>
public record PagedQuery
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Keyword { get; init; }
    public int? CategoryId { get; init; }
}

/// <summary>
/// 分页结果
/// </summary>
public record PagedResult<T>(List<T> Items, int TotalCount, int PageIndex, int PageSize)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
}
