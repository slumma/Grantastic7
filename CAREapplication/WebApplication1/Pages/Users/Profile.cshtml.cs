using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CAREapplication.Pages.Users
{
    public class ProfileModel : PageModel
    {
        public User activeUser { get; set; }
        public int activeUserID { get; set; } = new int();
        public List<CalendarEvent> calendarList { get; set; } = new List<CalendarEvent>();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            activeUser = DBClass.GetUserByID(HttpContext.Session.GetInt32("userID"));
            activeUserID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));

            SqlDataReader CalendarReader = DBClass.UserEventReader(activeUserID);
            while (CalendarReader.Read())
            {
                calendarList.Add(new CalendarEvent
                {
                    EventType = CalendarReader["EventType"].ToString(),
                    Title = CalendarReader["Title"].ToString(),
                    Start = DateTime.Parse(CalendarReader["StartDate"].ToString())
                });
            }
            DBClass.DBConnection.Close();


            return Page();

        }
    }
}
