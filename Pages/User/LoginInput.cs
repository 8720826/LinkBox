using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.User
{
    public class LoginInput
    {
        [Display(Name = "密码")]
        public string Password { get; set; } = "";
    }
}
