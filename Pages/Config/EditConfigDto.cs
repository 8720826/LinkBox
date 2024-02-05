using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Config
{
    public class EditConfigDto
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
