using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TestTask.Models
{
    [XmlRoot(ElementName = "guid")]
    public class Guid
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "isPermaLink")]
        public bool IsPermaLink { get; set; }

        [XmlText]
        public string? Text { get; set; }




        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
