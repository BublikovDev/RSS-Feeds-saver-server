using System.ComponentModel.DataAnnotations;

namespace TestTask.Models.Requests
{
    public class SetNewsAsReadRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool IsRead { get; set; }
    }
}
