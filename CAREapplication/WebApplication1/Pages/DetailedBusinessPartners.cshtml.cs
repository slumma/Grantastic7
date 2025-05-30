using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;


namespace CAREapplication.Pages
{


    public class DetailedBusinessPartnersModel : PageModel
    {
        public BusinessPartner funder { get; set; }
        public FunderNote note { get; set; }
        public IActionResult OnGet(int FunderID)
        {
            // Validate if the user is an admin trying to access the page
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("/Index"); // Redirect to login page
            }

            note = new FunderNote();
            using (SqlDataReader reader = DBFunder.SingleNoteReader(FunderID))
            {
                if (reader.Read())
                {
                    note.Contents = reader["Contents"].ToString();
                    note.DateAdded = DateTime.Parse(reader["DateAdded"].ToString());
                    note.FirstName = reader["FirstName"].ToString();
                    note.LastName = reader["LastName"].ToString();
                }
                reader.Close();

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
                    funder.FunderStatus = reader["StatusName"]?.ToString() ?? "Unknown";
                    funder.FirstName = reader["FirstName"].ToString();
                    funder.LastName = reader["LastName"].ToString();
                    funder.Email = reader["Email"].ToString();
                    funder.Phone = reader["Phone"].ToString();
                    funder.HomeAddress = reader["HomeAddress"].ToString();
                    funder.City = reader["City"].ToString();
                    funder.HomeState = reader["HomeState"].ToString();
                    funder.Zip = reader["Zip"].ToString();
                    funder.funderPOCID = Convert.ToInt32(reader["funderPOCID"]);
                }
                reader.Close();

            }
            DBFunder.DBConnection.Close();

            return Page();
        }
        public IActionResult OnPostUpdateCommStatus(int? funderPOCID, String? CommunicationStatus)
        {
            try
            {
                DBFunder.UpdateCommStatus(funderPOCID.Value, CommunicationStatus);
            }
            catch (SqlException ex)
            {
                Trace.WriteLine($"SQL Error (Update Task): {ex.Message}");
                ModelState.AddModelError("", "Error updating task: " + ex.Message);
            }

            return RedirectToPage(new { FunderID = funderPOCID });
        }

    }
}