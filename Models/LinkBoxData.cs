using LinkBox.Contexts;
using LinkBox.Entities;

namespace LinkBox.Models
{
	public static class LinkBoxData
	{

        public static List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

		public static List<LinkEntity> Links { get; set; } = new List<LinkEntity>();

		public static ConfigEntity Config { get; set; } = default!;


		public static void Refresh(bool force = false)
		{
            if (force)
            {
                Categories = new List<CategoryEntity>();
                Links = new List<LinkEntity>();
                Config = default!;
            }

            if (Categories.Count == 0 || Links.Count == 0 || Config == default!)
            {
                var db = new LinkboxDbContext();

                if (Categories.Count == 0)
                {
                    Categories = db.Categories.OrderBy(x => x.SortId).ToList();
                }

                if (Links.Count == 0)
                {
                    Links = db.Links.OrderBy(x=>x.SortId).ToList();
                }

                if (Config == default!)
                {
                    Config = db.Configs.FirstOrDefault();
                }
            }
        }
	}
}
