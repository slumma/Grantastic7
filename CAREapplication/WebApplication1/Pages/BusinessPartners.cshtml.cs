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
        public required List<BusinessPartner> bpList { get; set; } = new List<BusinessPartner>();

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
            SqlDataReader BPReader = DBFunder.BPReader();
            while (BPReader.Read())
            {
                bpList.Add(new BusinessPartner
                {
                    UserID = Int32.Parse(BPReader["UserID"].ToString()),
                    FirstName = BPReader["FirstName"].ToString(),
                    LastName = BPReader["LastName"].ToString(),
                    Email = BPReader["Email"].ToString(),
                    Phone = BPReader["Phone"].ToString(),
                    HomeAddress = BPReader["HomeAddress"].ToString(),
                    CommunicationStatus = BPReader["CommunicationStatus"].ToString(),
                    FunderID = Int32.Parse(BPReader["FunderID"].ToString()),
                    FunderName = BPReader["FunderName"].ToString(),
                    OrgType = BPReader["OrgType"].ToString(),
                    FunderStatus = BPReader["FunderStatus"].ToString()
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
                        FunderStatus = BPsearch["FunderStatus"].ToString()
                    });
                }
                DBFunder.DBConnection.Close(); // Load all projects so they still display
                return Page();
            }

            return Page();
        }
    }
}
