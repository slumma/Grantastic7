using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace CAREapplication.Pages
{


    public class DetailedBusinessPartnersModel : PageModel
    {
        public BusinessPartner BP { get; set; }
        public IActionResult OnGet(int SupplierID)
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

            BP = new BusinessPartner();
            using (SqlDataReader reader = DBGrantSupplier.SingleSupplierReader(SupplierID))
            {
                if (reader.Read())
                {
                    BP.OrgType = reader["OrgType"].ToString();
                }

                reader.Close();
            }
            DBGrantSupplier.DBConnection.Close();

            return Page();
        }

    }
}
