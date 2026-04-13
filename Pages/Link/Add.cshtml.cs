using LinkBox.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mapster;

namespace LinkBox.Pages.Link
{
    [UserAuthorize]
    public class AddModel : PageModel
    {
        [BindProperty]
        public CreateLinkRequest Link { get; set; } = default!;

        public SelectList Categories { get; set; } = default!;

        private readonly ILinkService _linkService;
        private readonly ICategoryService _categoryService;
        private readonly IHostEnvironment _hostEnvironment;

        public AddModel(ILinkService linkService, ICategoryService categoryService, IHostEnvironment hostEnvironment)
        {
            _linkService = linkService;
            _categoryService = categoryService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task OnGet()
        {
            await Init();
        }

        private async Task Init()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            Categories = new SelectList(categories, "Id", "Name");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await Init();
                return Page();
            }

            // 验证 URL 格式
            if (!Uri.TryCreate(Link.Url, UriKind.Absolute, out var uri))
            {
                ModelState.AddModelError("Url", "请填写有效的地址！");
                await Init();
                return Page();
            }

            // 处理从网页抓取信息
            if (Link.IsFetchIconFromLink || Link.IsFetchTitleFromLink || Link.IsFetchDescriptionFromLink)
            {
                var html = "";
                if (Link.IsFetchIconFromLink)
                {
                    var icon = $"{uri.ToBaseUrl()}/favicon.ico";
                    var iconUri = await icon.CheckAvailableAsync();

                    if (iconUri == null)
                    {
                        html = await Link.Url.DownloadHtmlAsync();
                        icon = html.ReadFavicon();
                        if (!string.IsNullOrEmpty(icon))
                        {
                            if (icon.ToLower().StartsWith("//"))
                            {
                                Link.Icon = $"{uri.Scheme}:{icon}";
                            }
                            else if (icon.ToLower().StartsWith("http"))
                            {
                                Link.Icon = icon;
                            }
                            else
                            {
                                Link.Icon = $"{uri.ToBaseUrl()}/{icon.TrimStart('/')}";
                            }
                        }
                    }
                    else
                    {
                        Link.Icon = icon;
                    }
                }

                if (Link.IsFetchTitleFromLink && string.IsNullOrEmpty(html))
                {
                    html = await Link.Url.DownloadHtmlAsync();
                    Link.Title = html.ReadTitle();
                }

                if (Link.IsFetchDescriptionFromLink && string.IsNullOrEmpty(html))
                {
                    html = await Link.Url.DownloadHtmlAsync();
                    Link.Description = html.ReadDescription();
                }
            }

            // 处理图标下载和保存
            if (!string.IsNullOrEmpty(Link.Icon) && Link.Icon.ToLower().StartsWith("http"))
            {
                var bytes = await Link.Icon.DownloadByteAsync();
                if (bytes != null)
                {
                    if (Link.IsSaveIconToBase64)
                    {
                        Link.Icon = $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
                    }
                    else if (Link.IsSaveIconToLocal)
                    {
                        var ms = new MemoryStream(bytes);
                        var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/icon");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        path = Path.Combine(path, $"{uri.Host}.ico");
                        using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                        {
                            ms.WriteTo(fs);
                        }
                        Link.Icon = $"/icon/{uri.Host}.ico";
                    }
                }
            }

            await _linkService.CreateLinkAsync(Link);
            return RedirectToPage("./Index");
        }
    }
}
