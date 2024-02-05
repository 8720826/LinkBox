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
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
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

            var password = _configuration["PASSWORD"]?.ToString() ?? "";

            if (password != User.Password)
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
