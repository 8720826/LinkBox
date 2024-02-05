using System.ComponentModel.DataAnnotations;

namespace LinkBox.Authorizations
{
    public class UserModel
    {


        [Display(Name = "密码")]
        public string Password { get; set; } = "";
    }
}
