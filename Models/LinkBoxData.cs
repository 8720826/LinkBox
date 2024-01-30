using LinkBox.Contexts;
using LinkBox.Entities;

namespace LinkBox.Models
{
	public static class LinkBoxData
	{
		public static List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

		public static List<LinkEntity> Links { get; set; } = new List<LinkEntity>();

		public static LinkBoxConfig Config { get; set; } = new LinkBoxConfig();


		public static void Refresh()
		{
            if (Categories.Count == 0 || Links.Count == 0 || Config.Count == 0)
            {
                var db = new LinkboxDbContext();

                if (Categories.Count == 0)
                {
                    Categories = db.Categories.ToList();
                }

                if (Links.Count == 0)
                {
                    Links = db.Links.ToList();
                }

                if (Config.Count == 0)
                {
                    var configs = db.Configs.ToList();

                    Config.Set(configs);
                }
            }
        }
	}
}
