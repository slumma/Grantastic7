using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CAREapplication.Pages.DB;
using CAREapplication.Pages.DataClasses;
using System.Data.SqlClient;
using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace CAREapplication.Pages.Project
{
    public class ProjectDashboardModel : PageModel
    {
        public required List<ProjectSimple> projectList { get; set; } = new List<ProjectSimple>();
        [BindProperty]
        public bool DisplayAll { get; set; }
        [BindProperty]
        public String TableButton { get; set; } = "Expand";

        [BindProperty]
        [Required(ErrorMessage = "You must have a search term.")]
        public String searchTerm { get; set; }
        public required List<ProjectSimple> searchedProjectList { get; set; } = new List<ProjectSimple>();
        public int director { get; set; }
        public int adminAssistant { get; set; }
        public int manager { get; set; }
        public int activeUserID { get; set; } = new int();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index");
            }

            activeUserID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            director = Convert.ToInt32(HttpContext.Session.GetInt32("director"));
            adminAssistant = Convert.ToInt32(HttpContext.Session.GetInt32("adminAssistant"));

            if (director == 1)
            {
                //Load Project List
                ProjectReader();

            }

            //GENERAL USER VIEW
            else
            {
                UserProjectReader(activeUserID);
            }

            DBProject.DBConnection.Close();

            Trace.WriteLine(projectList.Count);
            return Page();
        }

        public IActionResult OnPostToggleTable()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index");
            }

            LoadProjects();
            return Page();
        }

        public IActionResult OnPostSearch()
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ModelState.AddModelError("searchTerm", "Search term cannot be empty.");
                if (director == 1)
                {
                    ProjectReader();
                }
                else
                {
                    UserProjectReader(activeUserID);
                } 
                return Page();
            }

            SqlDataReader projectSearch = DBProject.projectSearch(searchTerm);
            while (projectSearch.Read())
            {
                searchedProjectList.Add(new ProjectSimple
                {
                    ProjectID = Int32.Parse(projectSearch["ProjectID"].ToString()),
                    ProjectName = projectSearch["ProjectName"].ToString(),
                    DueDate = DateTime.Parse(projectSearch["DueDate"].ToString()),
                    Amount = projectSearch["Amount"] != DBNull.Value && projectSearch["Amount"].ToString() != "" ? float.Parse(projectSearch["Amount"].ToString()) : 0f
                });
            }
            DBProject.DBConnection.Close();

            return Page();
        }

        private void LoadProjects()
        {
            SqlDataReader projectReader = DBProject.ProjectReader();
            while (projectReader.Read())
            {
                projectList.Add(new ProjectSimple
                {
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProjectName = projectReader["ProjectName"].ToString(),
                    DueDate = DateTime.Parse(projectReader["DueDate"].ToString()),
                    Amount = projectReader["Amount"] != DBNull.Value && projectReader["Amount"].ToString() != "" ? float.Parse(projectReader["Amount"].ToString()) : 0f
                });
            }
            DBProject.DBConnection.Close();
        }

        public void ProjectReader()
        {
            using (SqlDataReader reader = DBProject.ProjectReader())
            {
                while (reader.Read())
                {
                    projectList.Add(new ProjectSimple
                    {
                        ProjectID = Int32.Parse(reader["ProjectID"].ToString()),
                        ProjectName = reader["ProjectName"].ToString(),
                        DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                        Amount = reader["Amount"] != DBNull.Value && reader["Amount"].ToString() != "" ? float.Parse(reader["Amount"].ToString()) : 0f
                    });
                }
            }
            DBProject.DBConnection.Close();
        }


        public void UserProjectReader(int userID)
        {
            using (SqlDataReader reader = DBProject.UserProjectReader(Convert.ToInt32(HttpContext.Session.GetInt32("userID"))))
            {
                while (reader.Read())
                {
                    projectList.Add(new ProjectSimple
                    {
                        ProjectID = Int32.Parse(reader["ProjectID"].ToString()),
                        ProjectName = reader["ProjectName"].ToString(),
                        DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                        Amount = reader["Amount"] != DBNull.Value && reader["Amount"].ToString() != "" ? float.Parse(reader["Amount"].ToString()) : 0f
                    });
                }
            }
            DBProject.DBConnection.Close();
        }
    }
}