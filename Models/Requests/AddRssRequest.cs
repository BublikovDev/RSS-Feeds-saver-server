using System.ComponentModel.DataAnnotations;

namespace TestTask.Models.Requests
{
    public class AddRssRequest
    {
        [Required]
        [Url]
        public string FeedUrl { get; set; }
    }
}
