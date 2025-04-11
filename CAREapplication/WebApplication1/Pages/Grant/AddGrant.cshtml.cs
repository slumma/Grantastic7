using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.Grant
{
    public class AddGrantModel : PageModel
    {
        [BindProperty]
        public GrantSimple newGrant { get; set; }
        public List<GrantFunder> FunderList { get; set; } = new List<GrantFunder>();

        public List<ProjectSimple> ProjectList { get; set; } = new List<ProjectSimple>();

        public int? CurrentUserID { get; set; } = new int?();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            if (HttpContext.Session.GetInt32("director") != 1 && HttpContext.Session.GetInt32("adminAssistant") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Users/UserDashboard"); // Redirect to login page
            }

            // made a method at the bottom of the file so i dont have to copy and paste it a bunch of times 
            FunderList = LoadFunders();
            ProjectList = LoadProjects();

            return Page();
        }

        // executes when AddGrant is added, iinserts it into the db
        public IActionResult OnPostAddGrant()
        {
            FunderList = LoadFunders();
            ProjectList = LoadProjects();

            // debugging from AI
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Count > 0)
                {
                    Trace.WriteLine($"Key: {key}, Errors: {string.Join(", ", ModelState[key].Errors.Select(e => e.ErrorMessage))}");
                }
            }


            // if everything is valid in the form, add to db with the FunderID selected 
            if (ModelState.IsValid)
            {
                Trace.WriteLine("valid");

                // used AI for help with this, it associates the FunderName in the list with the FunderID
                GrantFunder selectedFunder = FunderList.FirstOrDefault(s => s.FunderName == newGrant.Funder);
                ProjectSimple selectedProject = ProjectList.FirstOrDefault(p => p.ProjectName == newGrant.Project);
                int FunderID = selectedFunder.FunderID;
                int projectID = selectedProject.ProjectID;

                DBGrant.InsertGrant(newGrant, FunderID, projectID, Convert.ToInt32(HttpContext.Session.GetInt32("userID")));
                return RedirectToPage("/Grant/GrantDashboard");
            }

            Trace.WriteLine("invalid");

            return Page();
        }

        // clears everything from form 
        public IActionResult OnPostClear()
        {
            ModelState.Clear();
            newGrant = new GrantSimple
            {
                GrantID = newGrant.GrantID, // Keep the GrantID the same
                Funder = string.Empty,
                Project = string.Empty,
                Amount = 0,
                Category = string.Empty,
                Status = string.Empty,
                Description = string.Empty,
                SubmissionDate = DateTime.Now,
                AwardDate = DateTime.Now
            };

            // if i wasnt reloading the Funder list it would throw an error, this was an easy fix 
            OnGet(); // just to reload the Funder list

            // Return the page with the cleared model
            return Page();
        }


        // populate button 
        public IActionResult OnPostPopulate()
        {
            ModelState.Clear();
            newGrant = new GrantSimple
            {
                GrantName = "Hello My Name is Carmen Winstead",
                Funder = "TechCorp",
                Project = "Project Alpha",
                Amount = 1000000,
                Category = "Federal",
                Status = "Pending",
                Description = "It's for the kids!",
                SubmissionDate = DateTime.Now,
                AwardDate = DateTime.Now
            };

            FunderList = LoadFunders();// Reload Funder list
            ProjectList = LoadProjects();

            return Page();
        }


        // make a method so i dont have to copy and paste it each time 
        // loads all of the Funders in the db into the Funderlist so it can be shown in the dropdown menu 
        private List<GrantFunder> LoadFunders()
        {
            var Funders = new List<GrantFunder>();
            using (SqlDataReader reader = DBFunder.FunderReader())
            {
                while (reader.Read())
                {
                    Funders.Add(new GrantFunder
                    {
                        FunderID = int.Parse(reader["FunderID"].ToString()),
                        FunderName = reader["FunderName"].ToString(),
                        OrgType = reader["OrgType"].ToString(),
                        BusinessAddress = reader["BusinessAddress"].ToString()
                    });
                }
            }

            DBFunder.DBConnection.Close();

            return Funders;
        }

        // make the same method for projects:
        private List<ProjectSimple> LoadProjects()
        {
            var projects = new List<ProjectSimple>();
            using (SqlDataReader reader = DBProject.ProjectReader())
            {
                while (reader.Read())
                {
                    projects.Add(new ProjectSimple
                    {
                        ProjectID = int.Parse(reader["ProjectID"].ToString()),
                        ProjectName = reader["ProjectName"].ToString()
                    });
                }
            }

            DBProject.DBConnection.Close();

            return projects;
        }
    }
}

