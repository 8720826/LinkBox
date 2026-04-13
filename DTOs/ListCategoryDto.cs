using LinkBox.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Category
{
    public class ListCategoryDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
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
