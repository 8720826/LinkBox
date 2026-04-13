using FluentValidation;
using LinkBox.DTOs;

namespace LinkBox.Validators;

/// <summary>
/// 创建链接请求验证器
/// </summary>
public class CreateLinkRequestValidator : AbstractValidator<CreateLinkRequest>
{
    public CreateLinkRequestValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("请选择分类");

        RuleFor(x => x.Title)
            .MaximumLength(512).WithMessage("标题最大长度不能超过512个字符");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("请输入地址")
            .MaximumLength(2048).WithMessage("地址最大长度不能超过2048个字符")
            .Must(BeAValidUrl).WithMessage("请输入有效的 URL 地址");

        RuleFor(x => x.Description)
            .MaximumLength(2048).WithMessage("描述最大长度不能超过2048个字符");

        RuleFor(x => x.Icon)
            .MaximumLength(2048).WithMessage("图标地址最大长度不能超过2048个字符");

        RuleFor(x => x.SortId)
            .GreaterThanOrEqualTo(0).WithMessage("排序值必须大于等于0");
    }

    private bool BeAValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}

/// <summary>
/// 更新链接请求验证器
/// </summary>
public class UpdateLinkRequestValidator : AbstractValidator<UpdateLinkRequest>
{
    public UpdateLinkRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("无效的链接 ID");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("请选择分类");

        RuleFor(x => x.Title)
            .MaximumLength(512).WithMessage("标题最大长度不能超过512个字符");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("请输入地址")
            .MaximumLength(2048).WithMessage("地址最大长度不能超过2048个字符")
            .Must(BeAValidUrl).WithMessage("请输入有效的 URL 地址");

        RuleFor(x => x.Description)
            .MaximumLength(2048).WithMessage("描述最大长度不能超过2048个字符");

        RuleFor(x => x.Icon)
            .MaximumLength(2048).WithMessage("图标地址最大长度不能超过2048个字符");

        RuleFor(x => x.SortId)
            .GreaterThanOrEqualTo(0).WithMessage("排序值必须大于等于0");
    }

    private bool BeAValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}

/// <summary>
/// 创建分类请求验证器
/// </summary>
public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("请输入分类名称")
            .MaximumLength(100).WithMessage("分类名称最大长度不能超过100个字符");

        RuleFor(x => x.SortId)
            .GreaterThanOrEqualTo(0).WithMessage("排序值必须大于等于0");
    }
}

/// <summary>
/// 更新分类请求验证器
/// </summary>
public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("无效的分类 ID");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("请输入分类名称")
            .MaximumLength(100).WithMessage("分类名称最大长度不能超过100个字符");

        RuleFor(x => x.SortId)
            .GreaterThanOrEqualTo(0).WithMessage("排序值必须大于等于0");
    }
}

/// <summary>
/// 分页查询参数验证器
/// </summary>
public class PagedQueryValidator : AbstractValidator<PagedQuery>
{
    public PagedQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("页码必须大于等于1");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("每页数量必须在 1-100 之间");

        RuleFor(x => x.Keyword)
            .MaximumLength(200).WithMessage("关键词最大长度不能超过200个字符");
    }
}
