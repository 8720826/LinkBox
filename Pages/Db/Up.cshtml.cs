using LinkBox.Authorizations;
using LinkBox.Migrator;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Db
{
    [UserAuthorize]
    public class UpModel : PageModel
    {
        private readonly IMigratorService _migrator;

        public UpModel(IMigratorService migrator) { _migrator = migrator; }
        public void OnGet()
        {
            _migrator.MigrateUp();
            LinkBoxData.Refresh(true);
        }
    }
}
