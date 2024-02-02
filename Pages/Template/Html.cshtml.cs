using LinkBox.Authorizations;
using LinkBox.Models.Template;
using LinkBox.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace LinkBox.Pages.Template
{
    [UserAuthorize]
    public class HtmlModel : PageModel
    {
        public readonly IHostEnvironment _hostEnvironment;
        public HtmlModel(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public ModifyTemplateModel Template { get; set; } = new ModifyTemplateModel();

        public void OnGet()
        {
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Template/index.html");
            Template.Html = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var result = TemplateProvider.Compile(Template.Html);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }

            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Template/index.html");
            System.IO.File.WriteAllText(path, Template.Html);

            return RedirectToPage("/template/html");
        }
    }
}
