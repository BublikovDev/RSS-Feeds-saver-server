using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TestTask.Models
{
    [XmlRoot(ElementName = "source")]
    public class Source
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "url")]
        public string? Url { get; set; }

        [XmlText]
        public string? Text { get; set; }



        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
