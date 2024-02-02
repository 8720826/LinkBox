using System.ComponentModel.DataAnnotations;

namespace LinkBox.Models
{
    public class ModifyCssModel
    {
        [DataType(DataType.Html)]
        public string Css { get; set; }
    }
}
