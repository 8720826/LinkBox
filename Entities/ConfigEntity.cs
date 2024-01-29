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

        public string Name { get; set; } = "";

        public ConfigTypeEnum Type { get; set; }


        public string Value { get; set; } = "";
    }
}
