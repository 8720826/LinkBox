using LinkBox.Entities;
using LinkBox.Models;
using Microsoft.Extensions.Hosting;
using RazorEngineCore;

namespace LinkBox.Template
{
    public class TemplateProvider
    {
        public static DateTime NextCompileTime { get; set; } = DateTime.MinValue;

        public static string Compile(string html,string css,string js)
        {
            LinkBoxData.Refresh();
            var appCategories = LinkBoxData.Categories.Where(x => x.Type == Entities.Enums.CategoryTypeEnum.应用).Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Links = LinkBoxData.Links.Where(y => y.CategoryId == x.Id).Select(y => new LinkModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Icon = y.Icon,
                    Title = y.Title,
                    Url = y.Url
                }).ToList()
            }).ToList();

            var bookmarkCategories = LinkBoxData.Categories.Where(x => x.Type == Entities.Enums.CategoryTypeEnum.书签).Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Links = LinkBoxData.Links.Where(y => y.CategoryId == x.Id).Select(y => new LinkModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Icon = y.Icon,
                    Title = y.Title,
                    Url = y.Url
                }).ToList()
            }).ToList();

            var config = new ConfigModel
            {
                Name = LinkBoxData.Config?.Name ?? "",
                Title = LinkBoxData.Config?.Title ?? "",
            };

            var model = new TemplateModel
            {
                AppCategories = appCategories,
                BookmarkCategories = bookmarkCategories,
                Config = config,
                Css = css,
                Js = js
            };

            var razorEngine = new RazorEngine();
            var template = razorEngine.Compile(html);

            var result = template.Run(model);

            return result;
        }


        public static string Reset(string contentRootPath, string file)
        {
            var defaultpath = Path.Combine(contentRootPath, "wwwroot", "template", file);
            var html = System.IO.File.ReadAllText(defaultpath, System.Text.Encoding.UTF8);

            var dir = Path.Combine(contentRootPath, "data", "template");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var newpath = Path.Combine(contentRootPath, "data", "template", file);
            System.IO.File.WriteAllText(newpath, html);

            return html;
        }

        public static void Update(string contentRootPath, string file, string content)
        {
            var newpath = Path.Combine(contentRootPath, "data", "template", file);
            System.IO.File.WriteAllText(newpath, content);
        }

        public static string Read(string contentRootPath, string file)
        {
            var path = Path.Combine(contentRootPath, "data", "template", file);
            return System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
        }


    }
}
