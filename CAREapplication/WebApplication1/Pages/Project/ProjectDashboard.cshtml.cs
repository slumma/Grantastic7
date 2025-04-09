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

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index");
            }
            DBProject.DBConnection.Close();
            LoadProjects();
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
                LoadProjects(); // Load all projects so they still display
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
    }
}