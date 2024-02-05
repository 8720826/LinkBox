using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Template
{
    public class EditHtmlDto
    {
        [Required(ErrorMessage = "请输入模板内容", AllowEmptyStrings = true)]
        [Display(Name = "内容")]
        public string Content { get; set; } = default!;
    }
}
