namespace TestTask.Models
{
    public class RSS
    {
        public int Id { get; set; }
        public string FeedUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}
