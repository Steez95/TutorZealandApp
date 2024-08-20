using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using TutorZealandApp.Models;

namespace TutorZealandApp.Pages.Admin.Sessions
{
    public class IndexSessionModel : PageModel
    {
        public List<SessionInfo> listSessions = new List<SessionInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbtutorzealandapp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM session";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                SessionInfo session = new SessionInfo();
                                session.id = reader.GetInt32(0);
                                session.subject = reader.GetString(1);
                                session.tutor = reader.GetString(2);
                                session.education = reader.GetString(3);
                                session.location = reader.GetString(4);
                                session.room = reader.GetString(5);
                                session.description = reader.GetString(7);

                                listSessions.Add(session);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { 

            }
        }
        public IActionResult OnPostDelete(int id)
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbtutorzealandapp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM session WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return Page();
            }

            return RedirectToPage();
        }
    }
    public class SessionInfo
    {
        public int id;
        public string? subject;
        public string? tutor;
        public string? education;
        public string? location;
        public string? room;
        public string? description;

    }
}
