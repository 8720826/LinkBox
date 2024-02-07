using LinkBox.Authorizations;
using LinkBox.Migrator;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Db
{
    [UserAuthorize]
    public class DownModel : PageModel
    {
        private readonly IMigratorService _migrator;
        private readonly IWebHostEnvironment _environment;
        public DownModel(IMigratorService migrator, IWebHostEnvironment environment) 
        {
            _migrator = migrator; 
            _environment = environment;
        }
        public void OnGet(int v = -1)
        {
            _migrator.MigrateDown(v);
            LinkBoxData.Refresh(true);

        }
    }
}
