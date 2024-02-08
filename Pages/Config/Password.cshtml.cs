using LinkBox.Contexts;
using LinkBox.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Config
{
    public class PasswordModel : PageModel
    {
        [BindProperty]
        public EditPasswordDto Config { get; set; } = new EditPasswordDto { };


        private readonly LinkboxDbContext _db;
        public PasswordModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet()
        {

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
                return RedirectToPage("./Index");
            }

            if(config.Password!= Config.Password.ToMd5())
            {
                ModelState.AddModelError("","√‹¬Î¥ÌŒÛ£°");
                return Page();
            }

            config.Password = Config.NewPassword.ToMd5();
            _db.Configs.Update(config);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
