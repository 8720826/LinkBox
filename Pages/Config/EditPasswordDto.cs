using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Config
{
    public class EditPasswordDto
    {

        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; } = "";


        [Required]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; } = "";

        [Required]
        [Compare("NewPassword", ErrorMessage = "两次密码输入的不一致")]
        [Display(Name = "重复新密码")]
        public string ConfirmPassword { get; set; } = "";
    }
}
