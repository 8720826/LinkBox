using BookmarksManager;
using LinkBox.Contexts;
using LinkBox.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IFormFile BookmarkFile { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        private void Init()
        {
            var categories = _db.Categories.ToList();

            Categories = new SelectList(categories, "Id", "Name");
        }


        public void OnGet()
        {
            Init();
        }

        public static string BinaryToChinese(string input)
        {
            StringBuilder sb = new StringBuilder();
            int numOfBytes = input.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            }
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        public async Task<IActionResult> OnPost()
        {

            if (BookmarkFile == null)
            {
                Init();
                ModelState.AddModelError("", "请上传文件！");
                return Page();
            }

            var Category = _db.Categories.Find(CategoryId);

            if (Category == null)
            {
                Init();
                ModelState.AddModelError("", "请选择分类！");
                return Page();
            }

            var html = "";
            using (var reader = new StreamReader(BookmarkFile.OpenReadStream()))
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
                    CategoryId = CategoryId
                });
                //Console.WriteLine("Url: {0}; Title: {1}", bookmark.Url, bookmark.Title);
            }
            _db.Links.AddRange(list);
            _db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
