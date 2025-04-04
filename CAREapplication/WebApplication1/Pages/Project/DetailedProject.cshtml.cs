using CAREapplication.Pages.DB;
using CAREapplication.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Identity.Client;


namespace CAREapplication.Pages.Project
{
    public class DetailedProjectModel : PageModel
    {
        public int ProjectID { get; set; }
        public ProjectSimple Project { get; set; }
        public List<User> UserProjectList { get; set; } = new List<User>();
        public List<User> UserTaskList { get; set; } = new List<User>();
        public List<ProjectTaskStaff> TaskStaffList { get; set; } = new List<ProjectTaskStaff>();
        public List<ProjectTask> TaskList { get; set; } = new List<ProjectTask>();
        public List<ProjectNote> NoteList { get; set; } = new List<ProjectNote>();
        public List<int> progressList { get; set; } = new List<int>();
        public string SupportingGrants { get; set; }
        public int progress { get; set; }
        public int total { get; set; }
        public int completed { get; set; }

        public IActionResult OnGet(int projectID)
        {

            Trace.WriteLine($"Received projectID: {projectID}");
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index");
            }
            else if (HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index");
            }

            ProjectID = projectID;
            Project = new ProjectSimple();

            List<int> progressList = new List<int>();

            try
            {
                Trace.WriteLine("Executing singleProjectReader query...");
                using (SqlDataReader reader = DBProject.singleProjectReader(projectID))
                {
                    if (reader.Read())
                    {
                        Project.ProjectName = reader["ProjectName"].ToString();
                        Project.DueDate = DateTime.Parse(reader["DueDate"].ToString());

                        // Check if 'Amount' is not NULL before setting it
                        if (!reader.IsDBNull(reader.GetOrdinal("Amount")))
                        {
                            Project.Amount = float.Parse(reader["Amount"].ToString());
                            SupportingGrants = reader["GrantNames"].ToString();
                        }
                        else
                        {
                            Project.Amount = 0; // Set a default value (e.g., 0) if Amount is NULL
                        }

                        Project.ProjectDescription = reader["ProjectDescription"].ToString();
                    }
                    reader.Close();
                }
                Trace.WriteLine("singleProjectReader query completed successfully.");

                DBProject.DBConnection.Close();


                Trace.WriteLine("Executing projectStaffReader query...");
                using (SqlDataReader reader = DBProject.projectStaffReader(projectID))
                {
                    while (reader.Read())
                    {
                        UserProjectList.Add(new User
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
                Trace.WriteLine("projectStaffReader query completed successfully.");

                DBProject.DBConnection.Close();

                Trace.WriteLine("Executing taskStaffReader query...");
                using (SqlDataReader reader = DBProject.taskStaffReader(projectID))
                {
                    while (reader.Read())
                    {
                        TaskStaffList.Add(new ProjectTaskStaff
                        {
                            TaskStaffID = Convert.ToInt32(reader["ProjectTaskStaffID"]),
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
                Trace.WriteLine("taskStaffReader query completed successfully.");

                DBProject.DBConnection.Close();

                Trace.WriteLine("Executing projectTaskReader query...");
                using (SqlDataReader reader = DBProject.projectTaskReader(projectID))
                {
                    while (reader.Read())
                    {
                        TaskList.Add(new ProjectTask
                        {
                            TaskID = Convert.ToInt32(reader["TaskID"]),
                            Objective = reader["Objective"].ToString(),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            Completed = Convert.ToInt32(reader["Completed"])
                        });
                    }
                }
                Trace.WriteLine("projectTaskReader query completed successfully.");

                DBProject.DBConnection.Close();

                Trace.WriteLine("Executing ProjectNoteReader query...");
                using (SqlDataReader reader = DBProject.ProjectNoteReader(projectID))
                {
                    while (reader.Read())
                    {
                        NoteList.Add(new ProjectNote
                        {
                            ProjectID = Convert.ToInt32(reader["ProjectID"]),
                            Content = reader["Content"].ToString(),
                            AuthorFirst = reader["FirstName"].ToString(),
                            AuthorLast = reader["LastName"].ToString(),
                            TimeAdded = Convert.ToDateTime(reader["NoteDate"])
                        });
                    }
                }
                Trace.WriteLine("ProjectNoteReader query completed successfully.");

                DBProject.DBConnection.Close();
            }
            catch (SqlException ex)
            {
                Trace.WriteLine($"SQL Error: {ex.Message}");
                foreach (SqlError error in ex.Errors)
                {
                    Trace.WriteLine($"SQL Error Detail: {error.Message}");
                }
                ModelState.AddModelError("", "An error occurred while retrieving project details: " + ex.Message);
            }

            progressList = DBProject.ProjectProgress(projectID);
            DBProject.DBConnection.Close();

            completed = progressList[0];
            total = progressList[1];

            progress = Convert.ToInt32(completed / total);

            return Page();
        }
    }
}
