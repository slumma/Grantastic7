using CAREapplication.Pages.DB;
using CAREapplication.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;



// where all users land when logging in as faculty
namespace CAREapplication.Pages.Grant
{
    public class GrantDashboardModel : PageModel
    {
        // initialize lists and variables to be used 

        public required List<GrantSimple> grantList { get; set; } = new List<GrantSimple>();

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public string CurrentSortOrder { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "You must have a search term.")]
        public String searchTerm { get; set; }

        public required List<GrantSimple> searchedGrantList { get; set; } = new List<GrantSimple>();

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
            HttpContext.Session.SetInt32("DisplayAll", 0);

            if (HttpContext.Session.GetInt32("adminStatus") == 1)
            {
                SqlDataReader grantReader = DBGrant.adminGrantReader();
                while (grantReader.Read())
                {
                    grantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null, // Handle NULL ProjectID
                        Funder = grantReader["Funder"].ToString(),
                        Project = grantReader["Project"].ToString(), // Handle NULL Project
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            else
            {
                // reads the db for grants for specific user
                int currentUserID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
                SqlDataReader grantReader = DBGrant.facGrantReader(currentUserID);
                while (grantReader.Read())
                {
                    grantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null, // Handle NULL ProjectID
                        Funder = grantReader["Funder"].ToString(),
                        Project = grantReader["Project"].ToString(), // Handle NULL Project
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }


            // Close your connection in DBClass
            DBGrant.DBConnection.Close();

            // links up to AI usage on the view, this switch statement allows the program to sort the grants by the selected sort order
            // allows for the columns to be sorted 
            switch (SortOrder)
            {
                case "amount_asc":
                    grantList = grantList.OrderBy(g => g.GrantName).ToList();
                    break;
                case "amount_desc":
                    grantList = grantList.OrderByDescending(g => g.Amount).ToList();
                    break;
                case "date_asc":
                    grantList = grantList.OrderBy(g => g.AwardDate).ToList();
                    break;
                case "date_desc":
                    grantList = grantList.OrderByDescending(g => g.AwardDate).ToList();
                    break;
                case "name_asc":
                    grantList = grantList.OrderBy(g => g.GrantName).ToList();
                    break;
                case "name_desc":
                    grantList = grantList.OrderByDescending(g => g.GrantName).ToList();
                    break;
                case "proj_asc":
                    grantList = grantList.OrderBy(g => g.Project).ToList();
                    break;
                case "proj_desc":
                    grantList = grantList.OrderByDescending(g => g.Project).ToList();
                    break;
                case "supp_asc":
                    grantList = grantList.OrderBy(g => g.Funder).ToList();
                    break;
                case "supp_desc":
                    grantList = grantList.OrderByDescending(g => g.Funder).ToList();
                    break;
                default:
                    grantList = grantList.OrderBy(g => g.GrantName).ToList();
                    break;
            }

            CurrentSortOrder = SortOrder;
            return Page();
        }


        public IActionResult OnPost()
        {
            // redirects to the detailed view page with the specific grant using asp-route
            return RedirectToPage("DetailedView");
        }

        public IActionResult OnPostSearch()
        {
            ModelState.Clear(); // Clear previous validation errors

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ModelState.AddModelError("searchTerm", "Search term cannot be empty.");

                // Load all grants to keep them visible
                grantList.Clear(); // Prevent duplicates
                SqlDataReader grantReader = DBGrant.adminGrantReader();

                while (grantReader.Read())
                {
                    grantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null,
                        Funder = grantReader["Funder"].ToString(),
                        Project = grantReader["Project"].ToString(),
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
                DBGrant.DBConnection.Close(); // Close connection

                return Page();
            }

            // If a search term is provided, perform project search
            searchedGrantList.Clear(); // Prevent old search results from persisting
            SqlDataReader projectSearch = DBGrant.searchGrant(searchTerm);

            if (!projectSearch.HasRows)
            {
                ModelState.AddModelError("searchTerm", "No projects found with the given search term.");
            }

            while (projectSearch.Read())
            {
                searchedGrantList.Add(new GrantSimple
                {
                    GrantID = Convert.ToInt32(projectSearch["GrantID"]),
                    GrantName = projectSearch["GrantName"].ToString(),
                    ProjectID = projectSearch["ProjectID"] != DBNull.Value ? Convert.ToInt32(projectSearch["ProjectID"]) : (int?)null,
                    Funder = projectSearch["Funder"].ToString(),
                    Project = projectSearch["Project"].ToString(),
                    Amount = Convert.ToSingle(projectSearch["Amount"]),
                    Category = projectSearch["Category"].ToString(),
                    Status = projectSearch["GrantStatus"].ToString(),
                    Description = projectSearch["descriptions"].ToString(),
                    SubmissionDate = Convert.ToDateTime(projectSearch["SubmissionDate"]),
                    AwardDate = Convert.ToDateTime(projectSearch["AwardDate"])
                });
            }
            DBGrant.DBConnection.Close(); // Close connection

            return Page();
        }


    }
}
