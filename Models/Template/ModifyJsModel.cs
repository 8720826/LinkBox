using System.ComponentModel.DataAnnotations;

namespace LinkBox.Models
{
    public class ModifyJsModel
    {
        [DataType(DataType.Html)]
        public string Js { get; set; }
    }
}
