using CAREapplication.Pages.DB;
using CAREapplication.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using global::CAREapplication.Pages.DataClasses;
using global::CAREapplication.Pages.DB;

namespace CAREapplication.Pages.Grant
{
    public class DetailedGrantModel : PageModel
    {
        // empty grant object to populate it
        public GrantSimple grant { get; set; }
        public List<GrantNote> noteList { get; set; } = new List<GrantNote>();

        public List<GrantStaff> staffList = new List<GrantStaff>();
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
            // fills the grant object with the info in the db so the user can see and edit it 
            grant = new GrantSimple(); // Initialize the grant object

            SqlDataReader grantReader = DBGrant.SingleGrantReader(grantID);

            while (grantReader.Read())
            {
                grant.GrantID = Int32.Parse(grantReader["GrantID"].ToString());
                grant.GrantName = grantReader["GrantName"].ToString();
                grant.ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null; // Handle NULL ProjectID
                grant.Supplier = grantReader["Supplier"].ToString();
                grant.Project = grantReader["Project"].ToString();
                grant.Amount = float.Parse(grantReader["Amount"].ToString());
                grant.Category = grantReader["Category"].ToString();
                grant.Status = grantReader["GrantStatus"].ToString();
                grant.Description = grantReader["descriptions"].ToString();
                grant.SubmissionDate = DateTime.Parse(grantReader["SubmissionDate"].ToString());
                grant.AwardDate = DateTime.Parse(grantReader["AwardDate"].ToString());
            }
            DBGrant.DBConnection.Close();

            SqlDataReader noteReader = DBGrant.GrantNoteReader(grantID);
            while (noteReader.Read())
            {
                noteList.Add(new GrantNote
                {
                    GrantID = Convert.ToInt32(noteReader["GrantID"]),
                    Content = noteReader["Content"].ToString(),
                    AuthorFirst = noteReader["FirstName"].ToString(),
                    AuthorLast = noteReader["LastName"].ToString(),
                    TimeAdded = Convert.ToDateTime(noteReader["NoteDate"].ToString())
                });
            }
            DBGrant.DBConnection.Close();

            staffList = grantStaffReader(grantID);

            return Page();
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("DetailedView");
        }

        public List<GrantStaff> grantStaffReader(int grantID)
        {
            List<GrantStaff> staffList = new List<GrantStaff>();

            SqlDataReader staffReader = DBFaculty.singleFacultyReader(grantID);

            while(staffReader.Read())
            {
                staffList.Add(new GrantStaff
                {
                    FirstName = staffReader["FirstName"].ToString(),
                    LastName = staffReader["LastName"].ToString(),
                    Email = staffReader["Email"].ToString(),
                    Phone = staffReader["Phone"].ToString(),
                    UserRole = staffReader["UserRole"].ToString()
                });
            }

            return staffList;
        }
    }
}
