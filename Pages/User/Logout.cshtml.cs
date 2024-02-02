using LinkBox.Authorizations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LinkBox.Pages.User
{
    [UserAuthorize]
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync();

            return RedirectToPage("/Dash/Index");
        }
    }
}
