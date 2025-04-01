using CAREapplication.Pages.DB;
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

        // Check for stored login error messages
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
        if (DBClass.UserCheck(Username))
        {
            if (DBClass.HashedLogin(Username, Password))
            {
                DBClass.DBConnection.Close();
                HttpContext.Session.SetInt32("loggedIn", 1);
                HttpContext.Session.SetString("username", Username);

                // Retrieve user ID
                int userID = DBClass.HashedUserID(Username);
                HttpContext.Session.SetInt32("userID", userID);
                DBClass.DBConnection.Close();

                // Check user permissions
                int adminStatus = DBClass.adminCheck(userID);
                DBClass.DBConnection.Close();

                if (adminStatus == 1)
                {
                    HttpContext.Session.SetInt32("adminStatus", 1);
                    return RedirectToPage("/Project/ProjectDashboard");
                }
                else
                {
                    int facultyStatus = DBClass.facultyCheck(userID);
                    DBClass.DBConnection.Close();

                    if (facultyStatus == 1)
                    {
                        HttpContext.Session.SetInt32("facultyStatus", 1);
                        return RedirectToPage("/Faculty/FacultyLanding");
                    }
                    else
                    {
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
        else
        {
            ViewData["LoginMessage"] = "Username and/or Password Incorrect";
            return Page();
        }
    }

    public IActionResult OnPostLogoutHandler()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("Index");
    }
}
