using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class AddProjectNoteModel : PageModel
    {
        [BindProperty]
        public ProjectNote newProjectNote { get; set; } = new ProjectNote();
        public ProjectSimple project { get; set; } = new ProjectSimple();
        public User user { get; set; } = new User();
        public int ProjectID { get; set; }
        public IActionResult OnGet(int? ProjectID)
        {
            // control validating if the user is an admin trying to access the page 
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

            // checks the project ID, if null, assign it to the value in session state, if false set the session state to the ID
            if (ProjectID == null || ProjectID == 0)
            {
                // help from AI (Copilot) to set the projectID to 0 if it is null
                ProjectID = HttpContext.Session.GetInt32("projectID") ?? 0;
            }
            else
            {
                HttpContext.Session.SetInt32("projectID", ProjectID.Value);
            }

            // store projectID 
            this.ProjectID = Convert.ToInt32(ProjectID);

            // populate project Name
            SqlDataReader projectReader = DBProject.singleProjectReader(Convert.ToInt32(ProjectID));
            while (projectReader.Read())
            {
                project.ProjectName = projectReader["ProjectName"].ToString();
            }
            DBProject.DBConnection.Close();

            user = DBClass.GetUserByID(Convert.ToInt32(HttpContext.Session.GetInt32("userID")));
            DBClass.DBConnection.Close();

            newProjectNote.ProjectID = Convert.ToInt32(ProjectID);
            newProjectNote.AuthorFirst = user.FirstName;
            newProjectNote.AuthorLast = user.LastName;
            newProjectNote.AuthorID = user.UserID;
            newProjectNote.TimeAdded = DateTime.Now;

            return Page();
        }

        public IActionResult OnPost()
        {
            // if inputs are valid 
            if (ModelState.IsValid)
            {
                DBProject.InsertProjectNote(newProjectNote);
                // anonymous identity to pass to the Project Detail page
                // used Copilot to resolve errors, this was the most *barbaric* way of doing it with things that we knew
                return RedirectToPage("ProjectDetail", new { ProjectID = newProjectNote.ProjectID });
            }
            return Page();
        }

        public IActionResult OnPostClear()
        {

            // clears the inputs 
            ModelState.Clear();
            newProjectNote = new ProjectNote();
            return RedirectToPage("AddProjectNote", new { ProjectID = HttpContext.Session.GetInt32("projectID") });
        }
    }
}
