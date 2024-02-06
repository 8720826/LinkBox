using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Template
{
    public class EditJsDto
    {


        [Display(Name = "内容")]
        public string? Content { get; set; }

        [Display(Name = "立即更新页面")]
        public bool IsCompileImmediately { get; set; }
    }
}
