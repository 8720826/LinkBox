using LinkBox.Contexts;
using LinkBox.Entities.Enums;
using LinkBox.Entities;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LinkBox.Authorizations;
using LinkBox.Pages.Link;
using Mapster;

namespace LinkBox.Pages.Config
{
    [UserAuthorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public EditConfigDto Config { get; set; } = default!;


        private readonly LinkboxDbContext _db;
        public IndexModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet()
        {
            var config = _db.Configs.FirstOrDefault();
            if (config == null)
            {
                config = new ConfigEntity() { Id = 0 };
                _db.Configs.Add(config);
                _db.SaveChanges();
            }

            Config = config.Adapt<EditConfigDto>();

            return Page();
        }



        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var config = Config.Adapt<ConfigEntity>();
            _db.Configs.Update(config);
            await _db.SaveChangesAsync();

            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
