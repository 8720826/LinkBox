using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Extentions;
using LinkBox.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LinkBox.Pages.User
{
    [UserAuthorize]
    public class PasswordModel : PageModel
    {
        private readonly LinkboxDbContext _db;
        private readonly IHttpContextAccessor _accessor;

        public PasswordModel(LinkboxDbContext db, IHttpContextAccessor accessor)
        {
            _db = db; 
            _accessor = accessor;
        }

        [BindProperty]
        public ModifyPasswordModel User { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int.TryParse(_accessor.HttpContext.User?.FindFirst($"Id")?.Value, out int id);

            var user = _db.Users.Find(id);
            if (user == null)
            {
                ModelState.AddModelError("", "用户名或密码错误！");
                return Page();
            }

            if (user.Password != User.Password.ToMd5())
            {
                ModelState.AddModelError("", "用户名或密码错误！");
                return Page();
            }


            if (User.NewPassword != User.ConfirmPassword)
            {
                ModelState.AddModelError("", "两次新密码必须相同！");
                return Page();
            }

            user.Password = User.NewPassword.ToMd5();
            _db.Users.Update(user);
            _db.SaveChanges();


            return RedirectToPage("/Dash/Index");
        }
    }
}
