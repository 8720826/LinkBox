using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Extentions;
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
        public LoginInput Login { get; set; }

        public void OnGet()
        {

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

            if (config.Password != Login.Password.ToMd5())
            {
                ModelState.AddModelError("", "密码错误！");
                return Page();
            }

            var properties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.Now.AddDays(1),
                RedirectUri = "/dash/"
            };

            var identity = new ClaimsIdentity(new ClaimsIdentity(UserAuthenticationHandler.CustomerSchemeName));
            identity.AddClaim(new Claim("userid", Guid.NewGuid().ToString()));
            var claimsPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(UserAuthenticationHandler.CustomerSchemeName, claimsPrincipal, properties);

            return RedirectToPage("/Dash/Index");
        }
    }
}
