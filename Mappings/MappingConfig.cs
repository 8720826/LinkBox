using LinkBox.DTOs;
using LinkBox.Entities;

namespace LinkBox.Mappings;

/// <summary>
/// Mapster 映射配置
/// </summary>
public static class MappingConfig
{
    /// <summary>
    /// 注册映射
    /// </summary>
    public static void RegisterMappings()
    {
        // Link 映射
        TypeAdapterConfig<LinkEntity, LinkDto>.NewConfig()
            .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : null);

        TypeAdapterConfig<CreateLinkRequest, LinkEntity>.NewConfig();
        
        TypeAdapterConfig<UpdateLinkRequest, LinkEntity>.NewConfig()
            .Ignore(dest => dest.Id);

        // Category 映射
        TypeAdapterConfig<CategoryEntity, CategoryDto>.NewConfig();
        
        TypeAdapterConfig<CreateCategoryRequest, CategoryEntity>.NewConfig();
        
        TypeAdapterConfig<UpdateCategoryRequest, CategoryEntity>.NewConfig()
            .Ignore(dest => dest.Id);
    }
}
