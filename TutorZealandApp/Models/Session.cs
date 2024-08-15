namespace TutorZealandApp.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Tutor { get; set; }
        public string? Education { get; set; }
        public string? Location { get; set; }
        public string? Room { get; set; }
        public string? Description { get; set; }
    }
}
