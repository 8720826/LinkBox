using LinkBox.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Category
{
    public class EditCategoryDto
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(64, ErrorMessage = "最大长度不能超过{1}")]
        [Display(Name = "名称")]
        public string Name { get; set; } = "";


        [Display(Name = "排序")]
        [Required]
        public int SortId { get; set; }

        [Required]
        [Display(Name = "类型")]
        public CategoryTypeEnum Type { get; set; }
    }
}
