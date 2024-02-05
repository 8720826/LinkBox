using LinkBox.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Category
{
    public class DeleteCategoryDto
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; } = "";

        [DataType(DataType.Text)]

        [Display(Name = "排序")]
        public int SortId { get; set; }

        [Display(Name = "类型")]
        public CategoryTypeEnum Type { get; set; }
    }
}
