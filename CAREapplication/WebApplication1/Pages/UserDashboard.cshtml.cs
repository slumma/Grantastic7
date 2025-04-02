using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages
{
    public class UserDashboardModel : PageModel
    {
        public User activeUser { get; set; }

        public List<ProjectTask> ProjectTaskList { get; set; } = new List<ProjectTask>();
        public List<GrantTask> GrantTaskList { get; set; } = new List<GrantTask>();
        public void OnGet()
        {
            DBProject.DBConnection.Close();
            activeUser = DBClass.GetUserByID(HttpContext.Session.GetInt32("userID"));

            using (SqlDataReader reader = DBProject.UserTaskReader(HttpContext.Session.GetInt32("userID")))
            {
                while (reader.Read())
                {
                    if (reader["TaskType"].ToString() == "Grant Task")
                    {
                        ProjectTaskList.Add(new ProjectTask
                        {
                            TaskID = Convert.ToInt32(reader["TaskID"]),
                            ProjectID = Convert.ToInt32(reader["RelatedEntityID"]),
                            Objective = reader["Objective"].ToString(),
                            DueDate = Convert.ToDateTime(reader["DueDate"])
                        });
                    }
                    else if (reader["TaskType"].ToString() == "Project Task")
                    {
                        GrantTaskList.Add(new GrantTask
                        {
                            TaskID = Convert.ToInt32(reader["TaskID"]),
                            GrantID = Convert.ToInt32(reader["RelatedEntityID"]),
                            Objective = reader["Objective"].ToString(),
                            DueDate = Convert.ToDateTime(reader["DueDate"])
                        });
                    }
                }
            }
            DBProject.DBConnection.Close();

        }
    }
}
