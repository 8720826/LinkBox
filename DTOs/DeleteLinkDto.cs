using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Link
{
    public class DeleteLinkDto
    {
        [Display(Name = "ID")]
        public int Id { get; set; }


        [Display(Name = "名称")]
        public string Title { get; set; } = "";

        [Display(Name = "地址")]
        public string Url { get; set; } = "";

        [Display(Name = "立即更新页面")]
        public bool IsCompileImmediately { get; set; }
    }
}
