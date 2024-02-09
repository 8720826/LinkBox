using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Config
{
    public class EditPasswordDto
    {

        [Required(ErrorMessage = "请输入密码")]
        [Display(Name = "密码")]
        public string Password { get; set; } = "";


        [Required(ErrorMessage = "请输入新密码")]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; } = "";

        [Required(ErrorMessage = "请重复输入新密码")]
        [Compare("NewPassword", ErrorMessage = "两次密码输入的不一致")]
        [Display(Name = "重复新密码")]
        public string ConfirmPassword { get; set; } = "";
    }
}
