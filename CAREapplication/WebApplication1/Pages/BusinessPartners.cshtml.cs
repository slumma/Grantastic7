using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using CAREapplication.Pages.DB;

using CAREapplication.Pages.DataClasses;

namespace CAREapplication.Pages
{
    public class BusinessPartnersModel : PageModel
    {
        public required List<BusinessPartner> bpList { get; set; } = new List<BusinessPartner>();

        public IActionResult OnGet()
        {
            // Validate if the user is an admin trying to access the page
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("/Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("/Index"); // Redirect to login page
            }

            // Populate BusinessPartners to be shown in the view
            SqlDataReader BPReader = DBGrantSupplier.BPReader();
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
                    SupplierID = Int32.Parse(BPReader["SupplierID"].ToString()),
                    SupplierName = BPReader["SupplierName"].ToString(),
                    OrgType = BPReader["OrgType"].ToString(),
                    SupplierStatus = BPReader["SupplierStatus"].ToString()
                });
            }
            DBGrantSupplier.DBConnection.Close();
            return Page();
        }
    }
}
