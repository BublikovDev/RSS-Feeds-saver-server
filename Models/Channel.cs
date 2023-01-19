using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TestTask.Models
{
    [XmlRoot(ElementName = "channel")]
    public class Channel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [XmlElement(ElementName = "title")]
        public string? Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string? Link { get; set; }

        [XmlElement(ElementName = "description")]
        public string? Description { get; set; }

        [XmlElement(ElementName = "language")]
        public string? Language { get; set; }

        [NotMapped]
        [XmlElement(ElementName = "lastBuildDate")]
        public string? LastBuildDateString { get; set; }

        public DateTime LastBuildDate { get; set; }

        [NotMapped]
        [XmlElement(ElementName = "pubDate")]
        public string? PubDateString { get; set; }
        public DateTime PubDate { get; set; }

        [XmlElement(ElementName = "item")]
        public List<Item>? Items { get; set; }



        public int RssId { get; set; }
        public Rss Rss { get; set; }
    }
}
