using System.ComponentModel.DataAnnotations;

namespace LinkBox.Authorizations
{
    public class UserModel
    {
        public int Id { get; set; }

        [Display(Name = "用户名")]
        public string Name { get; set; } = "";


        [Display(Name = "密码")]
        public string Password { get; set; } = "";
    }
}
