using LinkBox.Common.Exceptions;
using LinkBox.Contexts;
using LinkBox.DTOs;
using LinkBox.Entities;
using LinkBox.Extentions;
using LinkBox.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LinkBox.Services.Implementations;

/// <summary>
/// 链接服务实现
/// </summary>
public class LinkService : ILinkService
{
    private readonly LinkboxDbContext _dbContext;
    private readonly ILogger<LinkService> _logger;

    public LinkService(LinkboxDbContext dbContext, ILogger<LinkService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<LinkDto>> GetAllLinksAsync()
    {
        var links = await _dbContext.Links
            .Include(l => l.Category)
            .OrderBy(l => l.SortId)
            .ToListAsync();

        return links.Adapt<List<LinkDto>>();
    }

    public async Task<PagedResult<LinkDto>> GetPagedLinksAsync(PagedQuery query)
    {
        var queryable = _dbContext.Links
            .Include(l => l.Category)
            .AsQueryable();

        if (query.CategoryId.HasValue)
        {
            queryable = queryable.Where(l => l.CategoryId == query.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            queryable = queryable.Where(l => 
                l.Title.Contains(query.Keyword) || 
                l.Description.Contains(query.Keyword) ||
                l.Url.Contains(query.Keyword));
        }

        var totalCount = await queryable.CountAsync();
        var items = await queryable
            .OrderBy(l => l.SortId)
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        var dtos = items.Adapt<List<LinkDto>>();

        return new PagedResult<LinkDto>(dtos, totalCount, query.PageIndex, query.PageSize);
    }

    public async Task<LinkDto?> GetLinkByIdAsync(int id)
    {
        var link = await _dbContext.Links
            .Include(l => l.Category)
            .FirstOrDefaultAsync(l => l.Id == id);

        return link?.Adapt<LinkDto>();
    }

    public async Task<int> CreateLinkAsync(CreateLinkRequest request)
    {
        // 验证 URL 格式
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
        {
            throw new ValidationException("无效的 URL 格式");
        }

        var link = request.Adapt<LinkEntity>();
        
        _dbContext.Links.Add(link);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("创建链接成功，ID: {LinkId}", link.Id);
        return link.Id;
    }

    public async Task UpdateLinkAsync(UpdateLinkRequest request)
    {
        var link = await _dbContext.Links.FindAsync(request.Id);
        if (link == null)
        {
            throw new NotFoundException($"未找到 ID 为 {request.Id} 的链接");
        }

        // 验证 URL 格式
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
        {
            throw new ValidationException("无效的 URL 格式");
        }

        link.CategoryId = request.CategoryId;
        link.Title = request.Title;
        link.Url = request.Url;
        link.Description = request.Description;
        link.Icon = request.Icon;
        link.SortId = request.SortId;

        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("更新链接成功，ID: {LinkId}", request.Id);
    }

    public async Task DeleteLinkAsync(int id)
    {
        var link = await _dbContext.Links.FindAsync(id);
        if (link == null)
        {
            throw new NotFoundException($"未找到 ID 为 {id} 的链接");
        }

        _dbContext.Links.Remove(link);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("删除链接成功，ID: {LinkId}", id);
    }

    public async Task CheckLinkAvailabilityAsync(int id)
    {
        var link = await _dbContext.Links.FindAsync(id);
        if (link == null)
        {
            throw new NotFoundException($"未找到 ID 为 {id} 的链接");
        }

        try
        {
            var isAvailable = await link.Url.CheckAvailableAsync();
            link.IsAvailable = isAvailable != null;
            link.LastCheckTime = DateTime.Now;
            
            if (link.IsAvailable)
            {
                link.LastAvailableTime = DateTime.Now;
            }

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "检查链接可用性失败，ID: {LinkId}", id);
            link.IsAvailable = false;
            link.LastCheckTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 批量创建链接
    /// </summary>
    public async Task<int> BulkCreateLinksAsync(List<CreateLinkRequest> requests)
    {
        if (requests == null || !requests.Any())
        {
            return 0;
        }

        var entities = new List<LinkEntity>();
        foreach (var request in requests)
        {
            if (Uri.TryCreate(request.Url, UriKind.Absolute, out _))
            {
                entities.Add(request.Adapt<LinkEntity>());
            }
        }

        if (!entities.Any())
        {
            return 0;
        }

        await _dbContext.Links.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("批量创建链接成功，数量：{Count}", entities.Count);
        return entities.Count;
    }
}
