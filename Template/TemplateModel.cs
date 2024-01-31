using LinkBox.Entities;
using LinkBox.Models;

namespace LinkBox.Template
{
    public class TemplateModel
    {
        public List<CategoryModel> AppCategories { get; set; } = new List<CategoryModel>();

        public  List<CategoryModel> BookmarkCategories { get; set; } = new List<CategoryModel>();

        public  LinkBoxConfig Config { get; set; } = new LinkBoxConfig();
    }

    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public List<LinkModel> Links { get; set; } = new List<LinkModel>();
    }

    public class LinkModel
    {
        public int Id { get; set; }


        public string Title { get; set; } = "";

        public string Url { get; set; } = "";

        public string Description { get; set; } = "";

        public string Icon { get; set; } = "";

    }
}
