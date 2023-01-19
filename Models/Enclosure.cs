using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TestTask.Models
{
    [XmlRoot(ElementName = "enclosure")]
    public class Enclosure
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "url")]
        public string? Url { get; set; }

        [XmlAttribute(AttributeName = "length")]
        public int Length { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string? Type { get; set; }



        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
