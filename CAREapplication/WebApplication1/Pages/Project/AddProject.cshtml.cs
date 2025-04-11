using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.Project
{
    public class AddProjectModel : PageModel
    {
        [BindProperty]
        public ProjectSimple NewProject { get; set; } = new ProjectSimple();

        [BindProperty]
        public List<int> SelectedUserIDs { get; set; } = new List<int>();
        [BindProperty]
        public int StaffUserID { get; set; }
        public List<GrantSimple> AllGrants { get; set; } = new List<GrantSimple>();
        public List<User> AllUsers { get; set; } = new List<User>();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index");
            }

            using (SqlDataReader reader = DBClass.UserReader())
            {
                while (reader.Read())
                {
                    AllUsers.Add(new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    });
                }
                DBClass.DBConnection.Close();
            }
            PopulateDropdownData();
            return Page();
        }

        public IActionResult OnPostAddProject()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdownData(); // So it doesn’t crash on redisplay
                return Page();
            }

            var assignedFacultyList = new List<int> { StaffUserID };

            try
            {
                DBProject.AddProject(NewProject, assignedFacultyList);
                // You can also handle GrantID here if needed
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error adding project: {ex.Message}");
                ModelState.AddModelError("", "There was an error adding the project.");
                PopulateDropdownData();
                return Page();
            }

            return RedirectToPage("ProjectDashboard");
        }

        public void PopulateDropdownData()
        {
            // load users
            using (SqlDataReader reader = DBClass.UserReader())
            {
                while (reader.Read())
                {
                    AllUsers.Add(new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["Username"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    });
                }
            }

            DBClass.DBConnection.Close();

            // load grants
            using (SqlDataReader reader = DBGrant.allGrantReader())
            {
                while (reader.Read())
                {
                    AllGrants.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(reader["GrantID"]),
                        GrantName = reader["GrantName"].ToString()
                    });
                }
            }

            DBGrant.DBConnection.Close();
        }

    }
}
