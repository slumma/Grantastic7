using lab484.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public IActionResult OnGet(string logout)
    {
        if (logout == "true")
        {
            HttpContext.Session.Clear();
            ViewData["LoginMessage"] = "Successfully Logged Out!";
        }

        string loginError = HttpContext.Session.GetString("LoginError");
        if (!string.IsNullOrEmpty(loginError))
        {
            ViewData["LoginMessage"] = loginError;
            HttpContext.Session.Remove("LoginError"); // Clear after displaying
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        string loginQuery = "SELECT COUNT(*) FROM users WHERE Username = @Username AND Password = @Password";
        string findUserID = "SELECT UserID FROM users WHERE Username = @Username AND Password = @Password";

        // check if login credentials are valid
        if (DBClass.HashedLogin(Username, Password))
        {
            DBClass.DBConnection.Close();
            HttpContext.Session.SetInt32("loggedIn", 1);
            HttpContext.Session.SetString("username", Username);

            // retrieve userIDs
            int userID = DBClass.HashedUserID(Username);
            HttpContext.Session.SetInt32("userID", userID);
            DBClass.DBConnection.Close();

            // check user permissions
            int adminStatus = DBClass.adminCheck(userID);
            DBClass.DBConnection.Close();
            if (adminStatus == 1)
            {
                HttpContext.Session.SetInt32("adminStatus", 1);
                return RedirectToPage("/Admin/AdminLanding");
            }
            else
            {
                int facultyStatus = DBClass.facultyCheck(userID);
                if (facultyStatus == 1)
                {
                    DBClass.DBConnection.Close();
                    HttpContext.Session.SetInt32("facultyStatus", 1);
                    return RedirectToPage("/Faculty/FacultyLanding");
                }
                else
                {
                    DBClass.DBConnection.Close();
                    return RedirectToPage("/NoPermissions");
                }
            }
        }
        else
        {
            ViewData["LoginMessage"] = "Username and/or Password Incorrect";
            return Page();
        }
    }


    public IActionResult OnPostLogoutHandler()
    {
        HttpContext.Session.Clear();
        return Page();
    }
}
