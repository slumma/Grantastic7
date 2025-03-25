using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class ProjectDetailModel : PageModel
    {
        public int projectID { get; set; }
        public ProjectSimple project { get; set; }
        public List<User> userProjectList { get; set; } = new List<User>();
        public List<User> userTaskList { get; set; } = new List<User>();
        public List<TaskStaff> taskStaffList { get; set; } = new List<TaskStaff>();
        public List<Tasks> taskList { get; set; } = new List<Tasks>();
        public List<ProjectNote> noteList { get; set; } = new List<ProjectNote>();
        public IActionResult OnGet(int projectID)
        {
            // control validating if the user is an admin trying to access the page 
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            this.projectID = projectID;

            project = new ProjectSimple();

            // populates the project object with details 
            SqlDataReader singleProjectReader = DBProject.singleProjectReader(projectID);
            while (singleProjectReader.Read())
            {
                project.ProjectName = singleProjectReader["ProjectName"].ToString();
                project.DueDate = DateTime.Parse(singleProjectReader["DueDate"].ToString());
                project.Amount = float.Parse(singleProjectReader["Amount"].ToString());
            }
            DBProject.DBConnection.Close();

            // populates the staff list 
            SqlDataReader projectStaffReader = DBProject.projectStaffReader(projectID);
            while (projectStaffReader.Read())
            {
                userProjectList.Add(new User
                {
                    FirstName = projectStaffReader["FirstName"].ToString(),
                    LastName = projectStaffReader["LastName"].ToString(),
                    Phone = projectStaffReader["Phone"].ToString(),
                    Email = projectStaffReader["Email"].ToString()
                });
            }
            DBProject.DBConnection.Close();

            // populates the task list
            SqlDataReader taskStaffReader = DBProject.taskStaffReader(projectID);
            while (taskStaffReader.Read())
            {
                taskStaffList.Add(new TaskStaff
                {
                    TaskStaffID = Int32.Parse(taskStaffReader["TaskStaffID"].ToString()),
                    TaskID = Int32.Parse(taskStaffReader["TaskID"].ToString()),
                    AssigneeID = Int32.Parse(taskStaffReader["AssigneeID"].ToString()),
                    AssignerID = Int32.Parse(taskStaffReader["AssignerID"].ToString()),
                    DueDate = DateTime.Parse(taskStaffReader["DueDate"].ToString())
                });
                userTaskList.Add(new User
                {
                    FirstName = taskStaffReader["FirstName"].ToString(),
                    LastName = taskStaffReader["LastName"].ToString()
                });
            }
            DBProject.DBConnection.Close();

            // populate the taskList
            SqlDataReader taskReader = DBProject.taskReader(projectID);
            while (taskReader.Read())
            {
                taskList.Add(new Tasks
                {
                    TaskID = Int32.Parse(taskReader["TaskID"].ToString()),
                    Objective = taskReader["Objective"].ToString(),
                    DueDate = DateTime.Parse(taskReader["DueDate"].ToString())
                });
            }
            DBProject.DBConnection.Close();

            // populate the notes list with notes left on the project
            SqlDataReader noteReader = DBProject.ProjectNoteReader(projectID);
            while (noteReader.Read())
            {
                noteList.Add(new ProjectNote
                {
                    ProjectID = Convert.ToInt32(noteReader["ProjectID"]),
                    Content = noteReader["Content"].ToString(),
                    AuthorFirst = noteReader["FirstName"].ToString(),
                    AuthorLast = noteReader["LastName"].ToString(),
                    TimeAdded = Convert.ToDateTime(noteReader["NoteDate"].ToString())
                });
            }
            DBProject.DBConnection.Close();

            return Page();


        }
    }
}
