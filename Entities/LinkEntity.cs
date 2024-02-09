using LinkBox.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkBox.Entities
{
    [Table("Link")]
    public class LinkEntity
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; } = "";

        public string Url { get; set; } = "";

        public string Description { get; set; } = "";

        public string Icon { get; set; } = "";

        public int SortId { get; set; }


        public bool IsAvailable { get; set; }

        public DateTime LastCheckTime { get; set; }

        public DateTime LastAvailableTime { get; set; }

        public virtual CategoryEntity? Category { get; set; }

    }
}
