using CAREapplication.Pages.DB;
using CAREapplication.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using global::CAREapplication.Pages.DataClasses;
using global::CAREapplication.Pages.DB;
using System.Diagnostics;
using System.IO;

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
        public List<FileRecord> GrantFiles { get; set; } = new List<FileRecord>();
        public List<User> UserTaskList { get; set; } = new List<User>();
        public List<int> progressList { get; set; } = new List<int>();
        public List<User> AllUsers { get; set; } = new List<User>();
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
            // fills the grant object with the info in the db so the user can see and edit it 
            grant = new GrantSimple(); // Initialize the grant object
            DBGrant.DBConnection.Close();
            SqlDataReader grantReader = DBGrant.SingleGrantReader(grantID);

            int activeUserID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));


            while (grantReader.Read())
            {
                grant.GrantID = Int32.Parse(grantReader["GrantID"].ToString());
                grant.GrantName = grantReader["GrantName"].ToString();
                grant.ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null; // Handle NULL ProjectID
                grant.Funder = grantReader["Funder"].ToString();
                grant.Project = grantReader["Project"].ToString();
                grant.Amount = float.Parse(grantReader["Amount"].ToString());
                grant.Category = grantReader["Category"].ToString();
                grant.Status = grantReader["StatusName"].ToString();
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
                    TimeAdded = Convert.ToDateTime(noteReader["DateAdded"].ToString())
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

            GrantFiles = DBGrant.GetGrantFiles(grantID);

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

            UserReader(activeUserID);

            return Page();
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("GrantDashboard");
        }

        public List<GrantStaff> grantStaffReader(int grantID)
        {
            List<GrantStaff> staffList = new List<GrantStaff>();

            SqlDataReader staffReader = DBFaculty.singleFacultyReader(grantID);

            while (staffReader.Read())
            {
                staffList.Add(new GrantStaff
                {
                    UserID = Convert.ToInt32(staffReader["UserID"]),
                    FirstName = staffReader["FirstName"].ToString(),
                    LastName = staffReader["LastName"].ToString(),
                    Email = staffReader["Email"].ToString(),
                    Phone = staffReader["Phone"].ToString(),
                    UserRole = staffReader["UserRole"].ToString(),
                    PrincipalInvestigator = Convert.ToInt32(staffReader["PrincipalInvestigator"]),
                    CoPI = Convert.ToInt32(staffReader["CoPi"])
                });

            }
            DBFaculty.DBConnection.Close();

            return staffList;
        }

        public void UserReader(int userID)
        {
            using (SqlDataReader reader = DBClass.UserReader())
            {
                while (reader.Read())
                {
                    AllUsers.Add(new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["Username"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"].ToString()
                    });
                }
            }

            DBClass.DBConnection.Close();
        }
        public IActionResult OnPostAddNote(int grantID, string NoteContent)
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
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

        public IActionResult OnPostAddTask(int GrantID, DateOnly duedate, string objective)
        {
            try
            {
                int userID = (int)HttpContext.Session.GetInt32("userID");
                DBGrant.InsertGrantTaskAndAssignToAllStaff(GrantID, objective, duedate, userID);
                Trace.WriteLine("Executed");
            }
            catch (SqlException ex)
            {
                Trace.WriteLine($"SQL Error (Update Task): {ex.Message}");
                ModelState.AddModelError("", "Error updating task: " + ex.Message);
            }

            return RedirectToPage(new { projectID = GrantID });
        }

        public async Task<IActionResult> OnPostUploadFileAsync(IFormFile uploadedFile, int GrantID)
        {
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                ModelState.AddModelError("", "No file was selected.");
                return RedirectToPage(new { grantID = GrantID });
            }

            try
            {
                string folderPath = Path.Combine("wwwroot", "Resources", "GrantFiles", GrantID.ToString());

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Path.GetFileName(uploadedFile.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(stream);
                }

                string relativePath = Path.Combine("GrantFiles", GrantID.ToString(), fileName);
                DBGrant.InsertGrantFile(GrantID, relativePath, uploadedFile.ContentType, fileName);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"File upload failed: {ex.Message}");
            }

            return RedirectToPage(new { grantID = GrantID });
        }

        public IActionResult OnPostEditStaff(int GrantID, int UserID, string UserRole)
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index");
            }

            try
            {
                DBGrant.InsertGrantStaff(GrantID, UserID, UserRole);
            }
            catch (SqlException ex)
            {
                Trace.WriteLine($"SQL Error (Insert Grant Staff): {ex.Message}");
                ModelState.AddModelError("", "Error adding staff: " + ex.Message);
            }

            return RedirectToPage(new { grantID = GrantID });
        }

        public IActionResult OnPostEditOverview(int GrantID, string description, string category, string amount)
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index");
            }

            try
            {
                float parsedAmount = 0;
                float.TryParse(amount, out parsedAmount);

                DBGrant.UpdateGrantOverview(GrantID, description, category, parsedAmount);
            }
            catch (SqlException ex)
            {
                Trace.WriteLine($"SQL Error (Edit Overview): {ex.Message}");
                ModelState.AddModelError("", "Error updating grant overview: " + ex.Message);
            }

            return RedirectToPage(new { grantID = GrantID });
        }

        public IActionResult OnPostEditGrantPermissions(int GrantID, int UserID, string UserRole, bool PrincipalInvestigator, bool CoPI)
        {
            Trace.WriteLine($"Updating Grant Staff: GrantID={GrantID}, UserID={UserID}, Role={UserRole}, PI={PrincipalInvestigator}, CoPI={CoPI}");

            DBGrant.UpdateGrantStaffPermissions(GrantID, UserID, UserRole, PrincipalInvestigator, CoPI);

            return RedirectToPage(new { grantID = GrantID });
        }


        public IActionResult OnPostRemoveStaff(int GrantID, int UserID)
        {
            try
            {
                DBGrant.RemoveGrantStaff(GrantID, UserID);
            }
            catch (SqlException ex)
            {
                Trace.WriteLine($"SQL Error (Remove Grant Staff): {ex.Message}");
                ModelState.AddModelError("", "Error removing grant staff: " + ex.Message);
            }

            return RedirectToPage(new { grantID = GrantID });
        }


    }
}
