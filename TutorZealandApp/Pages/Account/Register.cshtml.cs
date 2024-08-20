using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TutorZealandApp.Models;
using System.Data.SqlClient;
using TutorZealandApp.MyHelpers;
using Microsoft.AspNetCore.Identity;

namespace TutorZealandApp.Pages.Account
{
    [RequireNoAuth]
    [BindProperties]
    public class RegisterModel : PageModel
    {

        [Required(ErrorMessage = "The Firstname is required")]
        public string Firstname { get; set; } = "";


        [Required(ErrorMessage = "The Firstname is required")]
        public string Lastname { get; set; } = "";


        [Required(ErrorMessage = "The Firstname is required"), EmailAddress]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; } = "";

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
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbtutorzealandapp;Integrated Security=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO register (firstname, lastname, email, password, role) " +
                                 "VALUES (@firstname, @lastname, @email, @password, 'student')";

                    var passwordHasher = new PasswordHasher<IdentityUser>();
                    string hashedPassword = passwordHasher.HashPassword(new IdentityUser(), Password);

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", Firstname);
                        command.Parameters.AddWithValue("@lastname", Lastname);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@password", hashedPassword);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(Email))
                {
                    errorMessage = "Email address already used";
                }
                else
                {
                    errorMessage = ex.Message;
                }

                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM register WHERE email=@email";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", Email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string firstname = reader.GetString(1);
                                string lastname = reader.GetString(2);
                                string email = reader.GetString(3);
                                string role = reader.GetString(4);

                                HttpContext.Session.SetInt32("id", id);
                                HttpContext.Session.SetString("firstname", firstname);
                                HttpContext.Session.SetString("lastname", lastname);
                                HttpContext.Session.SetString("email", email);
                                HttpContext.Session.SetString("role", role);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Account created successfully";
            Response.Redirect("/Index");
        }

    }
}
