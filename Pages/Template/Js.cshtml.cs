using LinkBox.Authorizations;
using LinkBox.Models;
using LinkBox.Models.Template;
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
        public ModifyJsModel Template { get; set; } = new ModifyJsModel();


        public string Message { get; set; } = "自定义脚本";


        public void OnGet()
        {
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Template/index.js");
            Template.Js = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
        }

        public async Task<IActionResult> OnPost()
        {
            Message = "自定义脚本";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Template/index.js");
            System.IO.File.WriteAllText(path, Template.Js);

            Message = "更新成功！";

            return Page();
        }
    }
}
