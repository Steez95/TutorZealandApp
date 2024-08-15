namespace TutorZealandApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int SubjectId { get; set; }

        public int SessionId { get; set; }
    }
}
