using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection;


namespace CAREapplication.Pages
{


    public class DetailedBusinessPartnersModel : PageModel
    {
        public BusinessPartner funder { get; set; }
        public IActionResult OnGet(int FunderID)
        {
            // Validate if the user is an admin trying to access the page
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("/Index"); // Redirect to login page
            }

            funder = new BusinessPartner();
            using (SqlDataReader reader = DBFunder.SingleFunderReader(FunderID))
            {
                if (reader.Read())
                {
                    funder.FunderID = Convert.ToInt32(reader["FunderID"]);
                    funder.FunderName = reader["FunderName"].ToString();
                    funder.OrgType = reader["OrgType"].ToString();
                    funder.BusinessAddress = reader["BusinessAddress"].ToString();
                    funder.CommunicationStatus = reader["CommunicationStatus"] != DBNull.Value
                        ? reader["CommunicationStatus"].ToString()
                        : "N/A";
                    funder.FunderStatus = reader["FunderStatus"]?.ToString() ?? "Unknown";
                    funder.FirstName = reader["FirstName"].ToString();
                    funder.LastName = reader["LastName"].ToString();
                    funder.Email = reader["Email"].ToString();
                    funder.Phone = reader["Phone"].ToString();
                    funder.HomeAddress = reader["HomeAddress"].ToString();
                    funder.City = reader["City"].ToString();
                    funder.HomeState = reader["HomeState"].ToString();
                    funder.Zip = reader["Zip"].ToString();
                }
                reader.Close();

            }
            DBFunder.DBConnection.Close();

            return Page();
        }

    }
}