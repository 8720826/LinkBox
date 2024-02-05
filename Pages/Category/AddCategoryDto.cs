using LinkBox.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Category
{
    public class AddCategoryDto
    {
        [MaxLength(64, ErrorMessage = "最大长度不能超过{1}")]
        [Display(Name = "名称")]
        public string Name { get; set; } = "";

        [DataType(DataType.Text)]

        [Display(Name = "排序")]
        public int SortId { get; set; }

        [Display(Name = "类型")]
        public CategoryTypeEnum Type { get; set; }
    }
}
