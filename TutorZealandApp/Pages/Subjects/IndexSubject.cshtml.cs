using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TutorZealandApp.Models;

namespace TutorZealandApp.Pages.Subjects
{
    public class IndexSubjectModel : PageModel
    {

        [BindProperty]
        public List<Subject> Subjects { get; set; } = new List<Subject>();

        public void OnGet()
        {

        }
    }
}
