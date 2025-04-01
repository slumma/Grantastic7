using CAREapplication.Pages.DB;
using CAREapplication.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;



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
                        Supplier = grantReader["Supplier"].ToString(),
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
                        Supplier = grantReader["Supplier"].ToString(),
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
                    grantList = grantList.OrderBy(g => g.Supplier).ToList();
                    break;
                case "supp_desc":
                    grantList = grantList.OrderByDescending(g => g.Supplier).ToList();
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
    }
}
