using System.ComponentModel.DataAnnotations;

namespace LinkBox.Models
{
    public class ModifyPasswordModel
    {
        public int Id { get; set; }



        [Display(Name = "密码")]
        public string Password { get; set; } = "";


        [Display(Name = "新密码")]
        public string NewPassword { get; set; } = "";


        [Display(Name = "重复密码")]
        public string ConfirmPassword { get; set; } = "";
    }
}
