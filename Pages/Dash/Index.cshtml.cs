using LinkBox.Authorizations;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Dash
{
    [UserAuthorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            LinkBoxData.Refresh();
        }
    }
}
