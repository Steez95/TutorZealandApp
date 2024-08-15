namespace TutorZealandApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TutorId { get; set; }
        public float Rating { get; set; }
        public string? Comment { get; set; }

    }
}
