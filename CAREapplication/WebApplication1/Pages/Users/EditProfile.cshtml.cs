using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

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

        public IActionResult OnPost()
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            activeUser = DBClass.GetUserByID(userID);

            // You can now access form values using Request.Form["FieldName"]
            string first = Request.Form["FirstName"];
            string last = Request.Form["LastName"];
            string username = Request.Form["Username"];
            string email = Request.Form["Email"];
            string phone = Request.Form["Phone"];
            string pronouns = Request.Form["Pronouns"];

            string address = Request.Form["HomeAddress"];
            string city = Request.Form["HomeCity"];
            string state = Request.Form["HomeState"];
            string zip = Request.Form["ZipCode"];

            Trace.WriteLine(zip);

            DBClass.UpdateUserInfo(userID, first, last, pronouns, username, email, phone, address, city, state, zip);

            return RedirectToPage("Profile");
        }
    }
}
