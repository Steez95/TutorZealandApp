using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TutorZealandApp.Models;

namespace TutorZealandApp.Pages.Admin.Sessions
{
    public class IndexSessionModel : PageModel
    {

        [BindProperty]
        public List<Session> Sessions { get; set; } = new List<Session>();

        public void OnGet()
        {
        }
    }
}
