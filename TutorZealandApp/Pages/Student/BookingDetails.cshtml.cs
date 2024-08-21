using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace TutorZealandApp.Pages.Admin.Sessions
{
    public class BookingDetailsModel : PageModel
    {
        public Session Session { get; set; }

        [BindProperty]
        public Student Student { get; set; }

        private readonly ISessionService _sessionService;

        public BookingDetailsModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Session = await _sessionService.GetSessionByIdAsync(id);

            if (Session == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Session = await _sessionService.GetSessionByIdAsync(id);
                return Page();
            }

            var result = await _sessionService.BookSessionAsync(id, Student);

            if (result)
            {
                return RedirectToPage("BookingConfirmation");
            }

            ModelState.AddModelError("", "There was an error booking the session.");
            Session = await _sessionService.GetSessionByIdAsync(id);
            return Page();
        }
    }

    public class Session
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Student
    {
        public string name { get; set; }
        public string email { get; set; }
    }

    public interface ISessionService
    {
        Task<Session> GetSessionByIdAsync(int id);
        Task<bool> BookSessionAsync(int id, Student student);
    }
}
