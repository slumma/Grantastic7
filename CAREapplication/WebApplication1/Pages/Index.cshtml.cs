using System.Diagnostics;
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
            ViewData["LogoutMessage"] = "Successfully Logged Out!";
        }

        // Check for stored login error messages
        string loginError = HttpContext.Session.GetString("LoginError");
        if (!string.IsNullOrEmpty(loginError))
        {
            ViewData["LoginMessage"] = loginError;
            HttpContext.Session.Remove("LoginError"); // Clear after displaying
        }

        string logoutMessage = HttpContext.Session.GetString("LogoutMessage");
        if (!string.IsNullOrEmpty(logoutMessage))
        {
            ViewData["LogoutMessage"] = logoutMessage;
            HttpContext.Session.Remove("LogoutMessage"); // Clear after displaying
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
                int director = DBClass.directorCheck(userID);
                DBClass.DBConnection.Close();

                int adminAssistant = DBClass.adminAsstCheck(userID);
                DBClass.DBConnection.Close();

                if (director == 1)
                {
                    HttpContext.Session.SetInt32("director", 1);
                    return RedirectToPage("/Users/UserDashboard");
                }
                else if (adminAssistant == 1)
                {
                    HttpContext.Session.SetInt32("adminAsst", 1);
                    return RedirectToPage("/Users/UserDashboard");
                }
                else { return RedirectToPage("/Users/UserDashboard"); }
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
        HttpContext.Session.SetString("LogoutMessage", "Successfully logged out.");
        return RedirectToPage("Index");
    }
}
