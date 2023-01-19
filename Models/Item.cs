using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TestTask.Models
{
    [XmlRoot(ElementName = "item")]
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [XmlElement(ElementName = "title")]
        public string? Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string? Link { get; set; }

        [XmlElement(ElementName = "description")]
        public string? Description { get; set; }

        [XmlElement(ElementName = "enclosure")]
        public Enclosure? Enclosure { get; set; }

        [XmlElement(ElementName = "guid")]
        public Guid? Guid { get; set; }

        [NotMapped]
        [XmlElement(ElementName = "pubDate")]
        public string? PubDateString { get; set; }
        public DateTime PubDate { get; set; }

        [XmlElement(ElementName = "source")]
        public Source? Source { get; set; }

        public bool IsRead { get; set; } = false;


        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}
