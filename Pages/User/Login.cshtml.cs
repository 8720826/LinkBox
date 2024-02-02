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
    public class LoginModel : PageModel
    {
        private readonly LinkboxDbContext _db;

        public LoginModel(LinkboxDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public UserModel User { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _db.Users.FirstOrDefault(x => x.Name == User.Name);
            if (user == null)
            {
                ModelState.AddModelError("", "用户名或密码错误！");
                return Page();
            }

            if (user.Password!= User.Password.ToMd5())
            {
                ModelState.AddModelError("", "用户名或密码错误！");
                return Page();
            }

            var properties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.Now.AddDays(1),
                RedirectUri = "/dash/"
            };

            var identity = new ClaimsIdentity(new ClaimsIdentity(UserAuthenticationHandler.CustomerSchemeName));
            identity.AddClaim(new Claim("userid", User.Id.ToString()));
            identity.AddClaim(new Claim("username", User.Name.ToString()));
            var claimsPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(UserAuthenticationHandler.CustomerSchemeName, claimsPrincipal, properties);

            return RedirectToPage("/Dash/Index");
        }
    }
}
