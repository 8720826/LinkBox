using LinkBox.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkBox.Entities
{
    [Table("Link")]
    public class LinkEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "分类")]
        public int CategoryId { get; set; }

        [Display(Name = "名称")]
        public string Title { get; set; } = "";

        [Display(Name = "地址")]
        public string Url { get; set; } = "";

        [Display(Name = "描述")]
        public string Description { get; set; } = "";

        [Display(Name = "图标")]
        public string Icon { get; set; } = "";

        [Display(Name = "排序")]
        public int SortId { get; set; }

    }
}
