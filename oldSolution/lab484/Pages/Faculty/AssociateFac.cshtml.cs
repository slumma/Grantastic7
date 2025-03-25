using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace lab484.Pages.Faculty
{
    public class AssociateFacModel : PageModel
    {
        [BindProperty]
        public int GrantID { get; set; }

        // make variable so we can access it w/o reading list 
        public string GrantName { get; set; }
        public required List<GrantStaff> StaffList { get; set; } = new List<GrantStaff>();
        public List<User> UserList { get; set; } = new List<User>();

        public IActionResult OnGet(int GrantID)
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("facultyStatus") != 1 && HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            this.GrantID = GrantID;      // sets the GrantID



            // populates the ProjectName variable so it can be used
            // checks if there are any staff in list first
            SqlDataReader grantReader = DBGrant.SingleGrantReader(GrantID);
            if (grantReader.Read())
            {
                GrantName = grantReader["GrantName"].ToString();
            }
            DBGrant.DBConnection.Close();

            SqlDataReader facultyReader = DBFaculty.singleFacultyReader(GrantID);
            while (facultyReader.Read())
            {
                StaffList.Add(new GrantStaff
                {
                    UserID = Int32.Parse(facultyReader["UserID"].ToString()),
                    Username = facultyReader["Username"].ToString(),
                    FirstName = facultyReader["FirstName"].ToString(),
                    LastName = facultyReader["LastName"].ToString(),
                    Email = facultyReader["Email"].ToString(),
                    Phone = facultyReader["Phone"].ToString(),
                    HomeAddress = facultyReader["HomeAddress"].ToString(),
                    GrantName = facultyReader["GrantName"].ToString()
                });
            }
            DBFaculty.DBConnection.Close();

            // as long as there are faculty in the db for the method to read from the user will be added to the userList 
            using (SqlDataReader facReader = DBFaculty.facReader())
            {
                while (facReader.Read())
                {
                    UserList.Add(new User
                    {
                        UserID = Int32.Parse(facReader["UserID"].ToString()),
                        UserName = facReader["Username"].ToString()
                    });
                }
            }

            // Close your connection in DBClass
            DBFaculty.DBConnection.Close();
            return Page();
        }

        public IActionResult OnPostAddFaculty(int UserID)
        {
            // Retrieve the User object based on UserID
            User user = DBClass.GetUserByID(UserID);

            Trace.WriteLine($"ProjectID: {this.GrantID}");
            Trace.WriteLine($"UserID: {UserID}");

            if (user != null)
            {
                // why does ProjectID keep showing as 0 omg 
                DBGrant.InsertGrantStaff(user, this.GrantID);
            }


            // reloads the page 
            return RedirectToPage(new
            {
                GrantID = this.GrantID
            });

        }
    }
}
