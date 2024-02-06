using LinkBox.Authorizations;
using LinkBox.Template;
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
        public EditCssDto Template { get; set; } = new EditCssDto();

        public string Message { get; set; } = "自定义样式";

        public void OnGet()
        {
            Template.Content = TemplateProvider.Read(_hostEnvironment.ContentRootPath, "index.css");
        }

        public IActionResult OnPost()
        {
            Message = "自定义样式";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            TemplateProvider.Update(_hostEnvironment.ContentRootPath, "index.css", Template.Content ?? "");

            TemplateProvider.NextCompileTime = DateTime.Now;


            Message = "更新成功！";

            return Page();
        }
    }
}
