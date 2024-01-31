using Home.Migrator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Db
{
    public class UpModel : PageModel
    {
        private readonly IMigratorService _migrator;

        public UpModel(IMigratorService migrator) { _migrator = migrator; }
        public void OnGet()
        {
            _migrator.MigrateUp();
        }
    }
}
