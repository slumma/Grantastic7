using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class AddProjectModel : PageModel
    {
        [BindProperty]
        public ProjectSimple newProject { get; set; }

        public List<User> facultyList { get; set; } = new List<User>();
        [BindProperty] public List<int> assignedFacultyList { get; set; } = new List<int>();

        // creates new project object and adds faculty to list
        public IActionResult OnGet()
        {
            // control validating if the user is an admin trying to access the page 
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            // populate the project variable with info 
            newProject = new ProjectSimple()
            {
                ProjectName = null,
                DueDate = DateTime.Now
            };
            // resets the facList
            facultyList.Clear();

            SqlDataReader facultyReader = DBFaculty.facReader();

            // populate the faculty list to see available people to add 
            while (facultyReader.Read())
            {
                facultyList.Add(new User
                {
                    FirstName = facultyReader["FirstName"].ToString(),
                    LastName = facultyReader["LastName"].ToString(),
                    UserID = Int32.Parse(facultyReader["UserID"].ToString())
                });
            }
            DBFaculty.DBConnection.Close();
            return Page();
        }

        public void OnPost()
        {
            
        }
         
        // add project button 
        public IActionResult OnPostAddProject()
        {
            // adds the project if it has employees selected to work on it 
            if (assignedFacultyList != null && assignedFacultyList.Any())
            {
                DBProject.AddProject(newProject, assignedFacultyList);
            }
            else
            {
                facultyList.Clear();
                SqlDataReader facultyReader = DBFaculty.facReader();
                while (facultyReader.Read())
                {
                    facultyList.Add(new User
                    {
                        FirstName = facultyReader["FirstName"].ToString(),
                        LastName = facultyReader["LastName"].ToString(),
                        UserID = Int32.Parse(facultyReader["UserID"].ToString())
                    });
                }
                DBFaculty.DBConnection.Close();
                return Page();
            }
            DBFaculty.DBConnection.Close();
            return RedirectToPage("AdminLanding");
        }

        // populates the fields 
        public IActionResult OnPostPopulate()
        {
            ModelState.Clear();
            newProject = new ProjectSimple
            {
                ProjectName = "Machine Project",
                DueDate = DateTime.Now,
            };

            //repopulate facultyList
            SqlDataReader facultyReader = DBFaculty.facReader();
            while (facultyReader.Read())
            {
                facultyList.Add(new User
                {
                    FirstName = facultyReader["FirstName"].ToString(),
                    LastName = facultyReader["LastName"].ToString(),
                    UserID = Int32.Parse(facultyReader["UserID"].ToString())
                });
            }
            DBFaculty.DBConnection.Close();

            return Page();
        }

        public IActionResult OnPostClear()
        {
            // Clear the ModelState
            ModelState.Clear();

            // Reset the model properties
            newProject = new ProjectSimple
            {
                DueDate = DateTime.Now
            };
            assignedFacultyList = new List<int>();

            //repopulate facultyList
            SqlDataReader facultyReader = DBFaculty.facReader();
            while (facultyReader.Read())
            {
                facultyList.Add(new User
                {
                    FirstName = facultyReader["FirstName"].ToString(),
                    LastName = facultyReader["LastName"].ToString(),
                    UserID = Int32.Parse(facultyReader["UserID"].ToString())
                });
            }
            DBFaculty.DBConnection.Close();

            return Page();
        }
    }
}
