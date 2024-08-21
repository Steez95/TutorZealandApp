using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TutorZealandApp.Pages.Account
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public void OnGet(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Please enter both email and password.";
                return Page();
            }

            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbtutorzealandapp;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Id, Password, Role, Firstname, Lastname FROM register WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader.GetString(1);
                                string role = reader.GetString(2);
                                string firstname = reader.GetString(3);
                                string lastname = reader.GetString(4);

                                var passwordHasher = new PasswordHasher<IdentityUser>();
                                var result = passwordHasher.VerifyHashedPassword(new IdentityUser(), storedHash, Password);

                                if (result == PasswordVerificationResult.Success)
                                {
                                    
                                    var claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name, Email),
                                        new Claim(ClaimTypes.Role, role),
                                        new Claim("FirstName", firstname),
                                        new Claim("LastName", lastname)
                                    };

                                    
                                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                    
                                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                                    
                                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                                    
                                    HttpContext.Session.SetInt32("id", reader.GetInt32(0));
                                    HttpContext.Session.SetString("firstname", firstname);
                                    HttpContext.Session.SetString("lastname", lastname);
                                    HttpContext.Session.SetString("email", Email);
                                    HttpContext.Session.SetString("role", role);

                                    
                                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                    {
                                        return Redirect(returnUrl);
                                    }

                                    return RedirectToPage("/Index");
                                }
                                else
                                {
                                    ErrorMessage = "Invalid email or password.";
                                }
                            }
                            else
                            {
                                ErrorMessage = "Invalid email or password.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred: " + ex.Message;
            }

            return Page();
        }
    }
}
