using LinkBox.Entities;
using LinkBox.Models;
using RazorEngineCore;

namespace LinkBox.Template
{
    public class TemplateProvider
    {
        public static string Compile(string html)
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

            var model = new TemplateModel { AppCategories = appCategories, BookmarkCategories = bookmarkCategories, Config = config };

            var razorEngine = new RazorEngine();
            var template = razorEngine.Compile(html);

            var result = template.Run(model);

            return result;
        }

    }
}
