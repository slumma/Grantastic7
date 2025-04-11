using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using CAREapplication.Pages.DB;

using CAREapplication.Pages.DataClasses;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CAREapplication.Pages
{
    public class BusinessPartnersModel : PageModel
    {
        public required List<BusinessPartner> funderList { get; set; } = new List<BusinessPartner>();

        [BindProperty]
        [Required(ErrorMessage = "You must have a search term.")]
        public String searchTerm { get; set; }

        public required List<BusinessPartner> searchedBPList { get; set; } = new List<BusinessPartner>();

        public IActionResult OnGet()
        {
            // Validate if the user is an admin trying to access the page
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("/Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("director") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("/Index"); // Redirect to login page
            }

            // Populate BusinessPartners to be shown in the view
            SqlDataReader FunderReader = DBFunder.FunderReader();
            while (FunderReader.Read())
            {
                funderList.Add(new BusinessPartner
                {
                    FunderID = int.Parse(FunderReader["FunderID"].ToString()),
                    FunderName = FunderReader["FunderName"].ToString(),
                    FunderStatus = FunderReader["StatusName"].ToString(), 
                    OrgType = FunderReader["OrgType"].ToString(),
                    BusinessAddress = FunderReader["BusinessAddress"].ToString(),

                    UserID = int.Parse(FunderReader["UserID"].ToString()),
                    CommunicationStatus = FunderReader["CommunicationStatus"].ToString(),

                    FirstName = FunderReader["FirstName"].ToString(),
                    LastName = FunderReader["LastName"].ToString(),
                    Email = FunderReader["Email"].ToString(),
                    Phone = FunderReader["Phone"].ToString(),
                    HomeAddress = FunderReader["HomeAddress"].ToString()
                });
            }
            DBFunder.DBConnection.Close();
            return Page();
        }

        public IActionResult OnPostSearch()
        {
            ModelState.Clear();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                Trace.WriteLine("AAAAAAAAAAAAHHHHHHHHHHHH");
                ModelState.AddModelError("searchTerm", "Search term cannot be empty.");
                SqlDataReader BPsearch = DBFunder.BPSearch(searchTerm);
                while (BPsearch.Read())
                {
                    searchedBPList.Add(new BusinessPartner
                    {
                        UserID = Int32.Parse(BPsearch["UserID"].ToString()),
                        FirstName = BPsearch["FirstName"].ToString(),
                        LastName = BPsearch["LastName"].ToString(),
                        Email = BPsearch["Email"].ToString(),
                        Phone = BPsearch["Phone"].ToString(),
                        HomeAddress = BPsearch["HomeAddress"].ToString(),
                        CommunicationStatus = BPsearch["CommunicationStatus"].ToString(),
                        FunderID = Int32.Parse(BPsearch["FunderID"].ToString()),
                        FunderName = BPsearch["FunderName"].ToString(),
                        OrgType = BPsearch["OrgType"].ToString(),
                        FunderStatus = BPsearch["StatusName"].ToString()
                    });
                }
                DBFunder.DBConnection.Close(); // Load all projects so they still display
                return Page();
            }

            return Page();
        }
    }
}
