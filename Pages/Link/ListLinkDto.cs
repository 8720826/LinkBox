using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Link
{
    public class ListLinkDto
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "分类")]
        public int CategoryId { get; set; }

        [Display(Name = "名称")]
        public string? Title { get; set; } = "";

        [Display(Name = "地址")]
        public string Url { get; set; } = "";

        [Display(Name = "分类")]
        public string? CategoryName { get; set; } = "";

        [Display(Name = "描述")]
        public string? Description { get; set; } = "";


        [Display(Name = "图标")]
        public string? Icon { get; set; } = "";


        [Display(Name = "排序")]
        public int SortId { get; set; }



        [Display(Name = "可访问")]
        public bool IsAvailable { get; set; }


        [Display(Name = "上次检查时间")]
        public DateTime LastCheckTime { get; set; }


        [Display(Name = "上次可访问时间")]
        public DateTime LastAvailableTime { get; set; }
    }
}
