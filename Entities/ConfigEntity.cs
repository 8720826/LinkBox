using LinkBox.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkBox.Entities
{
    [Table("Config")]
    public class ConfigEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "站点名称")]
        public string Name { get; set; } = "";


        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; } = "";
    }
}
