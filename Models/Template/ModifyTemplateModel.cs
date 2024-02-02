using System.ComponentModel.DataAnnotations;

namespace LinkBox.Models.Template
{
    public class ModifyTemplateModel
    {
        [DataType(DataType.Html)]
        public string Html { get; set; }
    }
}
