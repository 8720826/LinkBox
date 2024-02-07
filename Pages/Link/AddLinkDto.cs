using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Link
{
    public class AddLinkDto
    {
        [Required(ErrorMessage = "请选择分类")]
        [Display(Name = "分类")]
        public int CategoryId { get; set; }


        [MaxLength(2048, ErrorMessage = "最大长度不能超过{1}")]
        [Display(Name = "名称")]
        public string? Title { get; set; } = "";

        [Display(Name = "从网页读取名称")]
        public bool IsFetchTitleFromLink { get; set; }

        [Required(ErrorMessage = "请输入地址")]
        [MaxLength(2048, ErrorMessage = "最大长度不能超过{1}")]
        [Display(Name = "地址")]
        public string Url { get; set; } = "";


        [Display(Name = "描述")]
        [MaxLength(2048, ErrorMessage = "最大长度不能超过{1}")]
        public string? Description { get; set; } = "";


        [Display(Name = "图标")]
        public string? Icon { get; set; } = "";


        [Required(ErrorMessage = "请输入排序")]
        [Display(Name = "排序")]
        public int SortId { get; set; } = 99;

        [Display(Name = "立即更新页面")]
        public bool IsCompileImmediately { get; set; }


        [Display(Name = "从网页读取图标")]
        public bool IsFetchIconFromLink { get; set; }

        [Display(Name = "保存图标为Base64格式")]
        public bool IsSaveIconToBase64 { get; set; }

        [Display(Name = "从网页读取描述")]
        public bool IsFetchDescriptionFromLink { get; set; }
    }
}
