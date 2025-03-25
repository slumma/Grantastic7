using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace lab484.Pages.Faculty
{
    public class DetailedViewModel : PageModel
    {
        // empty grant object to populate it
        public GrantSimple grant { get; set; }
        public List<GrantNote> noteList { get; set; } = new List<GrantNote>();
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


            return Page();
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("DetailedView");
        }
    }
}