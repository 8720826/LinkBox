using LinkBox.Authorizations;
using LinkBox.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

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

        public IActionResult OnPost(string action)
        {
            Message = "自定义样式";
            if (action == "reset")
            {

                Template.Content = TemplateProvider.Reset(_hostEnvironment.ContentRootPath, "index.tpl");
                if (Template.IsCompileImmediately)
                {
                    TemplateProvider.NextCompileTime = DateTime.Now;
                }

                Message = "重置成功！";
                return Page();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                TemplateProvider.Update(_hostEnvironment.ContentRootPath, "index.css", Template.Content ?? "");

                if (Template.IsCompileImmediately)
                {
                    TemplateProvider.NextCompileTime = DateTime.Now;
                }


                Message = "更新成功！";

                return Page();
            }
        }
    }
}
