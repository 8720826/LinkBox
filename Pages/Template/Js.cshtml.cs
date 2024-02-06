using LinkBox.Authorizations;
using LinkBox.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Template
{
    [UserAuthorize]
    public class JsModel : PageModel
    {
        public readonly IHostEnvironment _hostEnvironment;
        public JsModel(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public EditJsDto Template { get; set; } = new EditJsDto();


        public string Message { get; set; } = "自定义脚本";


        public void OnGet()
        {
            Template.Content = TemplateProvider.Read(_hostEnvironment.ContentRootPath, "index.js");
        }

        public IActionResult OnPost()
        {
            Message = "自定义脚本";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            TemplateProvider.Update(_hostEnvironment.ContentRootPath, "index.js", Template.Content ?? "");

            TemplateProvider.NextCompileTime = DateTime.Now;

            Message = "更新成功！";

            return Page();
        }
    }
}
