using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Config
{
    [UserAuthorize]
    public class ModifyPasswordModel : PageModel
    {
        [BindProperty]
        public EditPasswordDto Config { get; set; } = new EditPasswordDto { };

        public string Message { get; set; } = "修改密码";

        private readonly LinkboxDbContext _db;
        public ModifyPasswordModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet()
        {

            return Page();
        }



        public async Task<IActionResult> OnPost()
        {
            Message = "修改密码";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var config = _db.Configs.FirstOrDefault();
            if (config == null)
            {
                ModelState.AddModelError("", "密码错误！");
                return Page();
            }

            if (config.Password != Config.Password.ToMd5())
            {
                ModelState.AddModelError("", "密码错误！");
                return Page();
            }

            config.Password = Config.NewPassword.ToMd5();
            _db.Configs.Update(config);
            await _db.SaveChangesAsync();

            Message = "修改成功";

            Config = new EditPasswordDto { };

            return Page();
        }
    }
}
