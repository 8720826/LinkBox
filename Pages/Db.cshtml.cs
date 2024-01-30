using Home.Migrator;
using LinkBox.Contexts;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages
{
    public class DbModel : PageModel
	{
		private readonly IMigratorService _migrator;

        public DbModel(IMigratorService migrator) { _migrator = migrator; }
		public void OnGet()
        {
            /*
			_migrator.MigrateDown(-1);

			_migrator.MigrateUp();
            */
            LinkBoxData.Refresh();

        }
    }
}
