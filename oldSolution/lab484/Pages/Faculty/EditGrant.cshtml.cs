using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Faculty
{
    public class EditGrantModel : PageModel
    {
        [BindProperty]
        public GrantSimple GrantToUpdate { get; set; }

        // takes the grantID as an argument to load the info from the DB to let the user edit it 
        public IActionResult OnGet(int grantID)
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
            // Fetch grant details using the grantID and populate GrantToUpdate
            SqlDataReader grantReader = DBGrant.SingleGrantReader(grantID);

            if (grantReader.HasRows)
            {
                while (grantReader.Read())
                {
                    GrantToUpdate = new GrantSimple
                    {
                        GrantID = Int32.Parse(grantReader["GrantID"].ToString()),
                        GrantName = grantReader["GrantName"].ToString(),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? (int?)Int32.Parse(grantReader["ProjectID"].ToString()) : null,
                        Supplier = grantReader["Supplier"].ToString(),
                        Project = grantReader["Project"] != DBNull.Value ? grantReader["Project"].ToString() : null,
                        Amount = float.Parse(grantReader["Amount"].ToString()),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = DateTime.Parse(grantReader["SubmissionDate"].ToString()),
                        AwardDate = DateTime.Parse(grantReader["AwardDate"].ToString())
                    };
                }
            }

            grantReader.Close();
            DBGrant.DBConnection.Close();
            return Page();
        }

        // when user presses save OnPost is executed 
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Update grant details
                DBGrant.UpdateGrant(GrantToUpdate);
                return RedirectToPage("FacultyLanding");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }
            return Page();
        }

        // clears all of the info in the form except the grantID
        public IActionResult OnPostClear()
        {
            ModelState.Clear();
            GrantToUpdate = new GrantSimple
            {
                GrantID = GrantToUpdate.GrantID, // Keep the GrantID the same
                Supplier = string.Empty,
                Project = string.Empty,
                Amount = 0,
                Category = string.Empty,
                Status = string.Empty,
                Description = string.Empty,
                SubmissionDate = DateTime.Now,
                AwardDate = DateTime.Now
            };

            // Return the page with the cleared model
            return Page();
        }
    }
}
