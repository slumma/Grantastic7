using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CAREapplication.Pages.Users
{
    public class EditProfileModel : PageModel
    {
        public User activeUser { get; set; }
        public IActionResult OnGet(int userID)
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            activeUser = DBClass.GetUserByID(userID);
            
            

            return Page();
        }
    }
}
