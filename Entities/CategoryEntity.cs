
using LinkBox.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkBox.Entities
{
    [Table("Category")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public int SortId { get; set; }

        public CategoryTypeEnum Type { get; set; }
    }
}
