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

        public string Message { get; set; } = "自定义样式";

        public void OnGet()
        {
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "data", "template/index.css");
            Template.Css = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
        }

        public async Task<IActionResult> OnPost()
        {
            Message = "自定义样式";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var path = Path.Combine(_hostEnvironment.ContentRootPath, "data", "template/index.css");
            System.IO.File.WriteAllText(path, Template.Css);

            Message = "更新成功！";

            return Page();
        }
    }
}
