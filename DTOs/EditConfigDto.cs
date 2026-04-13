using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Config
{
    public class EditConfigDto
    {


        [Required(ErrorMessage = "请填写站点名称")]
        [Display(Name = "站点名称")]
        public string Name { get; set; } = "";


        [Required(ErrorMessage = "请填写标题")]
        [Display(Name = "标题")]
        public string Title { get; set; } = "";
    }
}
