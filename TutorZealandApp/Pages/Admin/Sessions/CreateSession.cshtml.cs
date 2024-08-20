using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TutorZealandApp.Pages.Admin.Sessions
{
    [Authorize(Roles = "admin")]
    public class CreateSessionModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "The Subject is required")]
        public string Subject { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Tutor is required")]
        public string Tutor { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Education is required")]
        public string Education { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Location is required")]
        public string Location { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Room is required")]
        public string Room { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Description is required")]
        public string Description { get; set; } = "";

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Data validation failed";
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbtutorzealandapp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO session" +
                        "(subject, tutor, education, location, room, description) VALUES" +
                        "(@subject, @tutor, @education, @location, @room, @description);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@subject", Subject);
                        command.Parameters.AddWithValue("@tutor", Tutor);
                        command.Parameters.AddWithValue("@education", Education);
                        command.Parameters.AddWithValue("@location", Location);
                        command.Parameters.AddWithValue("@room", Room);
                        command.Parameters.AddWithValue("@description", Description);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }
            successMessage = "Data saved correctly";
            Response.Redirect("/Admin/Sessions/IndexSession");
        }
    }
}
