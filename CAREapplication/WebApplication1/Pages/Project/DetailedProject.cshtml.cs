using CAREapplication.Pages.DB;
using CAREapplication.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace CAREapplication.Pages.Project
{
    public class DetailedProjectModel : PageModel
    {
        public int ProjectID { get; set; }
        public ProjectSimple Project { get; set; }
        public List<User> UserProjectList { get; set; } = new List<User>();
        public List<User> UserTaskList { get; set; } = new List<User>();
        public List<TaskStaff> TaskStaffList { get; set; } = new List<TaskStaff>();
        public List<Tasks> TaskList { get; set; } = new List<Tasks>();
        public List<ProjectNote> NoteList { get; set; } = new List<ProjectNote>();

        public IActionResult OnGet(int projectID)
        {
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

            try
            {
                using (SqlDataReader reader = DBProject.singleProjectReader(projectID))
                {
                    if (reader.Read())
                    {
                        Project.ProjectName = reader["ProjectName"].ToString();
                        Project.DueDate = DateTime.Parse(reader["DueDate"].ToString());
                        Project.Amount = float.Parse(reader["Amount"].ToString());
                    }
                }

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

                using (SqlDataReader reader = DBProject.taskStaffReader(projectID))
                {
                    while (reader.Read())
                    {
                        TaskStaffList.Add(new TaskStaff
                        {
                            TaskStaffID = Convert.ToInt32(reader["TaskStaffID"]),
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

                using (SqlDataReader reader = DBProject.taskReader(projectID))
                {
                    while (reader.Read())
                    {
                        TaskList.Add(new Tasks
                        {
                            TaskID = Convert.ToInt32(reader["TaskID"]),
                            Objective = reader["Objective"].ToString(),
                            DueDate = Convert.ToDateTime(reader["DueDate"])
                        });
                    }
                }

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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while retrieving project details: " + ex.Message);
            }

            return Page();
        }
    }
}
