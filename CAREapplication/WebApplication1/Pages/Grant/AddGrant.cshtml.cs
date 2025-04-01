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
        public List<GrantSupplier> SupplierList { get; set; } = new List<GrantSupplier>();

        public List<ProjectSimple> ProjectList { get; set; } = new List<ProjectSimple>();

        public int? CurrentUserID { get; set; } = new int?();

        public IActionResult OnGet()
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
            // made a method at the bottom of the file so i dont have to copy and paste it a bunch of times 
            SupplierList = LoadSuppliers();
            ProjectList = LoadProjects();

            return Page();
        }

        // executes when AddGrant is added, iinserts it into the db
        public IActionResult OnPostAddGrant()
        {
            SupplierList = LoadSuppliers();
            ProjectList = LoadProjects();



            // debugging from AI
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Count > 0)
                {
                    Trace.WriteLine($"Key: {key}, Errors: {string.Join(", ", ModelState[key].Errors.Select(e => e.ErrorMessage))}");
                }
            }


            // if everything is valid in the form, add to db with the supplierID selected 
            if (ModelState.IsValid)
            {
                Trace.WriteLine("valid");

                // used AI for help with this, it associates the SupplierName in the list with the SupplierID
                GrantSupplier selectedSupplier = SupplierList.FirstOrDefault(s => s.SupplierName == newGrant.Supplier);
                ProjectSimple selectedProject = ProjectList.FirstOrDefault(p => p.ProjectName == newGrant.Project);
                int supplierID = selectedSupplier.SupplierID;
                int projectID = selectedProject.ProjectID;

                DBGrant.InsertGrant(newGrant, supplierID, projectID, Convert.ToInt32(HttpContext.Session.GetInt32("userID")));
                return RedirectToPage("FacultyLanding");
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
                Supplier = string.Empty,
                Project = string.Empty,
                Amount = 0,
                Category = string.Empty,
                Status = string.Empty,
                Description = string.Empty,
                SubmissionDate = DateTime.Now,
                AwardDate = DateTime.Now
            };

            // if i wasnt reloading the supplier list it would throw an error, this was an easy fix 
            OnGet(); // just to reload the supplier list

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
                Supplier = "TechCorp",
                Project = "Project Alpha",
                Amount = 1000000,
                Category = "Federal",
                Status = "Pending",
                Description = "It's for the kids!",
                SubmissionDate = DateTime.Now,
                AwardDate = DateTime.Now
            };

            SupplierList = LoadSuppliers();// Reload supplier list
            ProjectList = LoadProjects();

            return Page();
        }


        // make a method so i dont have to copy and paste it each time 
        // loads all of the suppliers in the db into the supplierlist so it can be shown in the dropdown menu 
        private List<GrantSupplier> LoadSuppliers()
        {
            var suppliers = new List<GrantSupplier>();
            using (SqlDataReader reader = DBGrantSupplier.GrantSupplierReader())
            {
                while (reader.Read())
                {
                    suppliers.Add(new GrantSupplier
                    {
                        SupplierID = int.Parse(reader["SupplierID"].ToString()),
                        SupplierName = reader["SupplierName"].ToString(),
                        OrgType = reader["OrgType"].ToString(),
                        BusinessAddress = reader["BusinessAddress"].ToString()
                    });
                }
            }

            DBGrantSupplier.DBConnection.Close();

            return suppliers;
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

