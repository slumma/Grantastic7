using CAREapplication.Pages.DB;
using CAREapplication.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using global::CAREapplication.Pages.DataClasses;
using global::CAREapplication.Pages.DB;
using System.Diagnostics;

namespace CAREapplication.Pages.Grant
{
    public class DetailedGrantModel : PageModel
    {
        // empty grant object to populate it

        public GrantSimple grant { get; set; }
        public List<GrantNote> noteList { get; set; } = new List<GrantNote>();
        public List<GrantStaff> staffList { get; set; } = new List<GrantStaff>();
        public List<GrantTask> TaskList { get; set; } = new List<GrantTask>();
        public List<GrantTaskStaff> TaskStaffList { get; set; } = new List<GrantTaskStaff>();
        public List<User> UserTaskList { get; set; } = new List<User>();
        public List<int> progressList { get; set; } = new List<int>();
        public int progress { get; set; }
        public int total { get; set; }
        public int completed { get; set; }
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
            DBGrant.DBConnection.Close();
            SqlDataReader grantReader = DBGrant.SingleGrantReader(grantID);

            while (grantReader.Read())
            {
                grant.GrantID = Int32.Parse(grantReader["GrantID"].ToString());
                grant.GrantName = grantReader["GrantName"].ToString();
                grant.ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null; // Handle NULL ProjectID
                grant.Funder = grantReader["Funder"].ToString();
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

            using (SqlDataReader reader = DBGrant.grantTaskReader(grantID))
            {
                while (reader.Read())
                {
                    TaskList.Add(new GrantTask
                    {
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        Objective = reader["Objective"].ToString(),
                        DueDate = Convert.ToDateTime(reader["DueDate"]),
                        Completed = Convert.ToInt32(reader["Completed"])
                    });
                }
            }
            DBGrant.DBConnection.Close();

            using (SqlDataReader reader = DBGrant.taskStaffReader(grantID))
            {
                while (reader.Read())
                {
                    TaskStaffList.Add(new GrantTaskStaff
                    {
                        TaskStaffID = Convert.ToInt32(reader["GrantTaskStaffID"]),
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        AssigneeID = Convert.ToInt32(reader["AssigneeID"]),
                        AssignerID = Convert.ToInt32(reader["AssignerID"]),
                        DueDate = Convert.ToDateTime(reader["DueDate"])
                    });
                    UserTaskList.Add(new User
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    });
                }
            }
            DBGrant.DBConnection.Close();

            staffList = grantStaffReader(grantID);

            progressList = DBGrant.GrantProgress(grantID);
            DBGrant.DBConnection.Close();

            completed = progressList[0];
            total = progressList[1];

            progress = Convert.ToInt32(completed / total);


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

            while (staffReader.Read())
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
            DBFaculty.DBConnection.Close();

            return staffList;
        }
        public IActionResult OnPostAddNote(int grantID, string NoteContent)
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index");
            }
            else if (HttpContext.Session.GetInt32("facultyStatus") != 1 && HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index");
            }

            try
            {
                int userID = (int)HttpContext.Session.GetInt32("userID");

                DBGrant.InsertGrantNote(grantID, NoteContent, userID);
                DBGrant.DBConnection.Close();
            }
            catch (SqlException ex)
            {
                Trace.WriteLine($"SQL Error (Insert Grant Note): {ex.Message}");
                ModelState.AddModelError("", "Error saving grant note: " + ex.Message);
            }

            return RedirectToPage(new { grantID = grantID });
        }

        public IActionResult OnPostUpdateTaskStatus(int? taskID, int? completeFlag, int? GrantID)
        {
            try
            {
                int userID = (int)HttpContext.Session.GetInt32("userID");
                DBGrant.UpdateGrantTask(taskID.Value, completeFlag.Value);
            }
            catch (SqlException ex)
            {
                Trace.WriteLine($"SQL Error (Update Task): {ex.Message}");
                ModelState.AddModelError("", "Error updating task: " + ex.Message);
            }

            return RedirectToPage(new { grantID = GrantID });
        }
    }
}
