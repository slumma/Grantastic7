using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab484.Pages.Faculty
{
    public class AddGrantNoteModel : PageModel
    {
        [BindProperty]
        public GrantNote newGrantNote { get; set; } = new GrantNote();
        public GrantSimple grant {  get; set; } = new GrantSimple();
        public User user { get; set; } = new User();
        public int GrantID { get; set; }
        public IActionResult OnGet(int? GrantID)
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

            if (GrantID == null)
            {
                GrantID = HttpContext.Session.GetInt32("grantID") ?? 0;
            }
            else
            {
                HttpContext.Session.SetInt32("grantID", Convert.ToInt32(GrantID));
            }


            this.GrantID = Convert.ToInt32(GrantID);

            SqlDataReader grantReader = DBGrant.SingleGrantReader(Convert.ToInt32(GrantID));
            while (grantReader.Read())
            {
                grant.GrantName = grantReader["GrantName"].ToString();
            }
            DBGrant.DBConnection.Close();

            user = DBClass.GetUserByID(Convert.ToInt32(HttpContext.Session.GetInt32("userID")));
            DBClass.DBConnection.Close();

            newGrantNote.GrantID = Convert.ToInt32(GrantID);
            newGrantNote.AuthorFirst = user.FirstName;
            newGrantNote.AuthorLast = user.LastName;
            newGrantNote.AuthorID = user.UserID;
            newGrantNote.TimeAdded = DateTime.Now;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                DBGrant.InsertGrantNote(newGrantNote);
                return RedirectToPage("DetailedView", new { GrantID = newGrantNote.GrantID });
            }
            return Page();
        }

        public IActionResult OnPostClear()
        {
            ModelState.Clear();
            newGrantNote = new GrantNote();
            return RedirectToPage("AddGrantNote", new { GrantID = HttpContext.Session.GetInt32("grantID") });
        }

    }
}
