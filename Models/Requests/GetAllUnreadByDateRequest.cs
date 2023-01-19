using System.ComponentModel.DataAnnotations;

namespace TestTask.Models.Requests
{
    public class GetAllUnreadByDateRequest
    {
        [Required]
        public DateTime Date { get; set; }
    }
}
