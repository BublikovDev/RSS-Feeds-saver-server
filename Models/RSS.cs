using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TestTask.Models
{
    [XmlRoot(ElementName = "rss")]
    public class Rss
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [XmlElement(ElementName = "channel")]
        public Channel Channel { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public double Version { get; set; }

        [XmlAttribute(AttributeName = "base")]
        public string? Base { get; set; }

        [XmlAttribute(AttributeName = "atom")]
        public string? Atom { get; set; }

        [XmlAttribute(AttributeName = "content")]
        public string? Content { get; set; }

        [XmlAttribute(AttributeName = "itunes")]
        public string? Itunes { get; set; }

        [XmlAttribute(AttributeName = "yandex")]
        public string? Yandex { get; set; }

        [XmlAttribute(AttributeName = "media")]
        public string? Media { get; set; }

        [XmlText]
        public string? Text { get; set; }

    }
}
