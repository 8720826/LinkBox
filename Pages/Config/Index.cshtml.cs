using LinkBox.Contexts;
using LinkBox.Entities.Enums;
using LinkBox.Entities;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBox.Pages.Config
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ConfigEntity Config { get; set; }




        private readonly LinkboxDbContext _db;

        public IndexModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet()
        {
            Config = _db.Configs.FirstOrDefault();
            if (Config == null)
            {
                Config = new ConfigEntity() { Id = 0 };
            }

            return Page();
        }



        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Config.Id == 0)
            {
                _db.Configs.Add(Config);
            }
            else
            {
                _db.Configs.Update(Config);
            }
            await _db.SaveChangesAsync();
            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
