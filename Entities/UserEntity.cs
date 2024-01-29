using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkBox.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";


        public string Password { get; set; } = "";
    }
}
