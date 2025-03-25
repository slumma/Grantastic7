using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class AdminLandingModel : PageModel
    {
        public required List<ProjectSimple> projectList { get; set; } = new List<ProjectSimple>();
        [BindProperty]
        public bool DisplayAll { get; set; }
        [BindProperty]
        public String TableButton { get; set; } = "Expand";
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

            SqlDataReader projectReader = DBProject.ProjectReader();
            while (projectReader.Read())
            {
                // populate the projectList 
                projectList.Add(new ProjectSimple
                {
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProjectName = projectReader["ProjectName"].ToString(),
                    DueDate = DateTime.Parse(projectReader["DueDate"].ToString()),
                    Amount = 0f
                });
                // if the amount isnt a value, try converting it to a string then parsing a float from it 
                if (projectReader["Amount"] != DBNull.Value)
                {
                    if (projectReader["Amount"].ToString() != "")
                    {
                        projectList.Last().Amount = float.Parse(projectReader["Amount"].ToString());
                    }
                    else
                    {
                        projectList.Last().Amount = 0f;
                    }
                }
                else
                {
                    projectList.Last().Amount = 0f;
                }

            }

            // Close your connection in DBClass
            DBProject.DBConnection.Close();
            return Page();
        }

        public IActionResult OnPostToggleTable()
        {

            // method for the collapse / expand button 
            // stores the choice of the user in session state 
            bool DisplayAll = (HttpContext.Session.GetInt32("DisplayAll") == 1);

            // if true, set the button to expand 
            if (DisplayAll)
            {
                TableButton = "Expand";
                HttpContext.Session.SetInt32("DisplayAll", 0);
            }
            // if false set the button title to collapse 
            else
            {
                TableButton = "Collapse";
                HttpContext.Session.SetInt32("DisplayAll", 1);
            }

            // same validation from the top of page 
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

            // loads projects 
            SqlDataReader projectReader = DBProject.ProjectReader();
            while (projectReader.Read())
            {
                // populate the projectList 
                projectList.Add(new ProjectSimple
                {
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProjectName = projectReader["ProjectName"].ToString(),
                    DueDate = DateTime.Parse(projectReader["DueDate"].ToString()),
                    Amount = 0f
                });
                // if the amount isnt a value, try converting it to a string then parsing a float from it 
                if (projectReader["Amount"] != DBNull.Value)
                {
                    if (projectReader["Amount"].ToString() != "")
                    {
                        projectList.Last().Amount = float.Parse(projectReader["Amount"].ToString());
                    }
                    else
                    {
                        projectList.Last().Amount = 0f;
                    }
                }
                else
                {
                    projectList.Last().Amount = 0f;
                }
            }
            DBProject.DBConnection.Close();
            return Page();
        }
    }
}
