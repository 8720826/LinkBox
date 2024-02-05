using LinkBox.Authorizations;
using LinkBox.Models;
using LinkBox.Models.Template;
using LinkBox.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Template
{
    [UserAuthorize]
    public class CssModel : PageModel
    {
        private readonly ITemplateService _templateService;
        public CssModel(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [BindProperty]
        public ModifyCssModel Template { get; set; } = new ModifyCssModel();

        public string Message { get; set; } = "自定义样式";

        public void OnGet()
        {
            Template.Css = _templateService.Read("index.css");
        }

        public async Task<IActionResult> OnPost()
        {
            Message = "自定义样式";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _templateService.Update("index.css", Template.Css);

            Message = "更新成功！";

            return Page();
        }
    }
}
