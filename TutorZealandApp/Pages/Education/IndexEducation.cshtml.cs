using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using TutorZealandApp.Models;

namespace TutorZealandApp.Pages.Education
{
    public class IndexEducationModel : PageModel
    {
        [BindProperty]
        public List<EducationInfo> listEducation { get; set; } = new List<EducationInfo>();

        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=.\\sqlexpress;Initial Catalog=dbtutorzealandapp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = " SELECT * FROM educations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EducationInfo educations = new EducationInfo();
                                educations.id = reader.GetInt32(0);
                                educations.education = reader.GetString(1);
                                educations.semester = reader.GetString(2);
                                educations.description = reader.GetString(3);

                                listEducation.Add(educations);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {

            }
        }
    }

    public class EducationInfo
    {
        public int id;
        public string education;
        public string semester;
        public string description;
    }
}
