using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.User
{
    public class LoginInput
    {
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; } = "";
    }
}
