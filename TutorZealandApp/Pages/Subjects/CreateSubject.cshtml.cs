using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TutorZealandApp.Models;

namespace TutorZealandApp.Pages.Subjects
{
    public class CreateSubjectModel : PageModel
    {

        [BindProperty]
        [Required(ErrorMessage = "The Subject is required")]
        public string Subject { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Subject is required")]
        public string education { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Subject is required")]
        public string semester { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Subject is required")]
        public string description { get; set; } = "";

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
