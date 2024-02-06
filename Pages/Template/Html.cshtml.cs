using LinkBox.Authorizations;
using LinkBox.Template;
using Microsoft.AspNetCore.Mvc;
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
        public EditHtmlDto Template { get; set; } = new EditHtmlDto();

        public string Message { get; set; } = "自定义模板";

        public void OnGet()
        {
            Template.Content = TemplateProvider.Read(_hostEnvironment.ContentRootPath, "index.tpl");
        }

        public IActionResult OnPost(string action)
        {
            Message = "自定义模板";
            if (action == "reset")
            {

                Template.Content = TemplateProvider.Reset(_hostEnvironment.ContentRootPath, "index.tpl");

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

                    var result = TemplateProvider.Compile(Template.Content, "", "");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return Page();
                }

                TemplateProvider.Update(_hostEnvironment.ContentRootPath, "index.tpl", Template.Content);

                TemplateProvider.NextCompileTime = DateTime.Now;

                Message = "更新成功！";
                return Page();
            }

        }
    }
}
