using LinkBox.Contexts;
using LinkBox.Models;

namespace LinkBox.Pages
{
	public class BasePageModel
	{
		public BasePageModel() 
		{

			if (LinkBoxData.Categories.Count == 0 || LinkBoxData.Links.Count == 0 || LinkBoxData.Config.Count == 0)
			{
				var db = new LinkboxDbContext();

				if (LinkBoxData.Categories.Count == 0)
				{
					LinkBoxData.Categories = db.Categories.ToList();
				}

				if (LinkBoxData.Links.Count == 0)
				{
					LinkBoxData.Links = db.Links.ToList();
				}

				if (LinkBoxData.Config.Count == 0)
				{
					var configs = db.Configs.ToList();

					LinkBoxData.Config.Set(configs);
				}
			}
		}
	}
}
