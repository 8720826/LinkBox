using LinkBox.Authorizations;
using LinkBox.Models;
using LinkBox.Models.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Template
{
    [UserAuthorize]
    public class CssModel : PageModel
    {
        public readonly IHostEnvironment _hostEnvironment;
        public CssModel(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public ModifyCssModel Template { get; set; } = new ModifyCssModel();

        public void OnGet()
        {
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Template/index.css");
            Template.Css = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Template/index.css");
            System.IO.File.WriteAllText(path, Template.Css);

            return RedirectToPage("/template/css");
        }
    }
}
