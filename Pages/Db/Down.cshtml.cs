using Home.Migrator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Db
{
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
            if (_environment.IsDevelopment())
            {
                _migrator.MigrateDown(v);
            }
           
        }
    }
}