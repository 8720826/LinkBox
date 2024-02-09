using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Extentions;
using LinkBox.Models;
using LinkBox.Template;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBox.Pages.Link
{
    [UserAuthorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public EditLinkDto Link { get; set; } = default!;

        public SelectList Categories { get; set; }



        private readonly IHostEnvironment _hostEnvironment;
        private readonly LinkboxDbContext _db;

        public EditModel(LinkboxDbContext db, IHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet(int id)
        {
            Init();
            var link = _db.Links.Find(id);
            if (link == null)
            {
                return RedirectToPage("Index");
            }

            Link = link.Adapt<EditLinkDto>();

            return Page();
        }

        private void Init()
        {
            var categories = _db.Categories.ToList();

            Categories = new SelectList(categories, "Id", "Name");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Init();
                return Page();
            }

            var uri = Link.Url.CheckFormat();
            if (uri == null)
            {
                ModelState.AddModelError("", "请填写有效的地址！");
                Init();
                return Page();
            }

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

                if (Link.IsFetchTitleFromLink)
                {
                    if (string.IsNullOrEmpty(html))
                    {
                        html = await Link.Url.DownloadHtmlAsync();
                    }
                    Link.Title = html.ReadTitle();
                }

                if (Link.IsFetchDescriptionFromLink)
                {
                    if (string.IsNullOrEmpty(html))
                    {
                        html = await Link.Url.DownloadHtmlAsync();
                    }
                    Link.Description = html.ReadDescription();
                }
            }

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

            var link = _db.Links.Find(Link.Id);
            Link.Adapt(link);

            link.Icon = link.Icon.CheckIsNullOrEmpty();
            link.Title = link.Title.CheckIsNullOrEmpty();
            link.Description = link.Description.CheckIsNullOrEmpty();


            _db.Links.Update(link);
            await _db.SaveChangesAsync();

            LinkBoxData.Refresh(true);
            if (Link.IsCompileImmediately)
            {
                TemplateProvider.NextCompileTime = DateTime.Now;
            }
            return RedirectToPage("./Index");
        }
    }
}
