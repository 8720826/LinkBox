using LinkBox.Bookmarks;
using LinkBox.Bookmarks.NetscapeFormat;
using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Models;
using LinkBox.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static NuGet.Packaging.PackagingConstants;

namespace LinkBox.Pages.Link
{
    public class ImportModel : PageModel
    {
        private readonly LinkboxDbContext _db;

        public ImportModel(LinkboxDbContext db)
        {
            _db = db;
        }

        public SelectList Categories { get; set; }


        [BindProperty]
        public ImportLinkDto Link { get; set; }


        private void Init()
        {
            var categories = _db.Categories.Where(x=>x.Type== Entities.Enums.CategoryTypeEnum.书签).ToList();

            Categories = new SelectList(categories, "Id", "Name");
        }


        public void OnGet()
        {
            Init();
        }

        public IActionResult OnPost()
        {

            if (Link.BookmarkFile == null)
            {
                Init();
                ModelState.AddModelError("", "请上传文件！");
                return Page();
            }

            var Category = _db.Categories.Find(Link.CategoryId);

            if (Category == null)
            {
                Init();
                ModelState.AddModelError("", "请选择分类！");
                return Page();
            }

            var html = "";
            using (var reader = new StreamReader(Link.BookmarkFile.OpenReadStream()))
            {
                html = reader.ReadToEnd();
            }
            var list = new List<LinkEntity>();
            var bookmarksReader = new NetscapeBookmarksReader();
            var bookmarks = bookmarksReader.Read(html);
            foreach (var bookmark in bookmarks.AllLinks)
            {
                if (string.IsNullOrEmpty(bookmark.Url))
                {
                    continue;
                }

                if (_db.Links.Any(x=>x.Url.ToLower()== bookmark.Url.ToLower()))
                {
                    continue;
                }

                var icon = bookmark.IconUrl ?? "";
                if (string.IsNullOrEmpty(icon))
                {
                    if (bookmark.IconDataBase64 != null)
                    {
                        icon = bookmark.IconDataBase64;
                    }
                }


                list.Add(new LinkEntity
                {
                    Description = bookmark.Description ?? "",
                    Url = bookmark.Url,
                    Title = bookmark.Title ?? "",
                    Icon = icon,
                    SortId = int.MaxValue,
                    CategoryId = Link.CategoryId
                });
                //Console.WriteLine("Url: {0}; Title: {1}", bookmark.Url, bookmark.Title);
            }
            _db.Links.AddRange(list);
            _db.SaveChanges();
            LinkBoxData.Refresh(true);

            if (Link.IsCompileImmediately)
            {
                TemplateProvider.NextCompileTime = DateTime.Now;
            }

            return RedirectToPage("./Index");
        }
    }
}
