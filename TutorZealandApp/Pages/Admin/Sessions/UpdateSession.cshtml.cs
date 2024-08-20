using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace TutorZealandApp.Pages.Admin.Sessions
{
    public class UpdateSessionModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

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

        public void OnGet(int id)
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbtutorzealandapp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM session WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                Subject = reader.GetString(1);
                                Tutor = reader.GetString(2);
                                Education = reader.GetString(3);
                                Location = reader.GetString(4);
                                Room = reader.GetString(5);
                                Description = reader.GetString(6);
                            }
                            else
                            {
                                errorMessage = "Session not found";
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Data validation failed";
                return Page();
            }

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbtutorzealandapp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE session SET subject = @subject, tutor = @tutor, education = @education, " +
                                 "location = @location, room = @room, description = @description WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@subject", Subject);
                        command.Parameters.AddWithValue("@tutor", Tutor);
                        command.Parameters.AddWithValue("@education", Education);
                        command.Parameters.AddWithValue("@location", Location);
                        command.Parameters.AddWithValue("@room", Room);
                        command.Parameters.AddWithValue("@description", Description);
                        command.Parameters.AddWithValue("@id", Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }

            successMessage = "Session updated successfully";
            return RedirectToPage("/Admin/Sessions/IndexSession");
        }
    }
}
