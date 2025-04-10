using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CAREapplication.Pages.Users
{
    public class ProfileModel : PageModel
    {
        public User activeUser { get; set; }
        public int activeUserID { get; set; } = new int();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            activeUser = DBClass.GetUserByID(HttpContext.Session.GetInt32("userID"));
            activeUserID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            return Page();
        }
    }
}
