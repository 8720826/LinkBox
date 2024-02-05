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
        private readonly ITemplateService _templateService;
        public HtmlModel(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [BindProperty]
        public ModifyTemplateModel Template { get; set; } = new ModifyTemplateModel();

        public string Message { get; set; } = "自定义模板";

        public void OnGet()
        {
            Template.Html = _templateService.Read("index.html");
        }

        public async Task<IActionResult> OnPost(string action)
        {
            Message = "自定义模板";
            if (action == "reset")
            {

                Template.Html = _templateService.Reset("index.html");

                Message = "重置成功！";
                return Page();
            }
            else
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

                _templateService.Update("index.html", Template.Html);

                Message = "更新成功！";
                return Page();
            }

        }
    }
}
