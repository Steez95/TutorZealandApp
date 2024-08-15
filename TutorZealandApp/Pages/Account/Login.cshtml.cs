using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace TutorZealandApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential{ get; set; }

        public void OnGet()
        {
        }
        
        public void OnPost()
        {

        }
    }

    public class Credential
    {
        [Required]
        public string Brugernavn { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Adgangskode { get; set; }
    }
}
