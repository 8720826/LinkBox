using LinkBox.Entities;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LinkBox.Authorizations;
using Mapster;
using LinkBox.Extentions;
using LinkBox.Contexts;

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

            var config = _db.Configs.FirstOrDefault();
            if (config == null)
            {
                config = new ConfigEntity() { Id = 1, Password = "admin".ToMd5() };
                _db.Configs.Add(config);
                _db.SaveChanges();
            }
            Config.Adapt(config);
            _db.Configs.Update(config);
            await _db.SaveChangesAsync();

            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
