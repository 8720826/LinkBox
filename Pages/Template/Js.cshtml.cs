using LinkBox.Authorizations;
using LinkBox.Models;
using LinkBox.Models.Template;
using LinkBox.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Template
{
    [UserAuthorize]
    public class JsModel : PageModel
    {
        private readonly ITemplateService _templateService;
        public JsModel(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [BindProperty]
        public ModifyJsModel Template { get; set; } = new ModifyJsModel();


        public string Message { get; set; } = "自定义脚本";


        public void OnGet()
        {
            Template.Js = _templateService.Read("index.js");
        }

        public async Task<IActionResult> OnPost()
        {
            Message = "自定义脚本";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _templateService.Update("index.js", Template.Js);

            Message = "更新成功！";

            return Page();
        }
    }
}
