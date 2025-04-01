using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CAREapplication.Pages
{
    public class UserDashboardModel : PageModel
    {
        public User activeUser { get; set; }

        public List<Tasks> UserTaskList { get; set; } = new List<Tasks>();

        public void OnGet()
        {
            activeUser = DBClass.GetUserByID(HttpContext.Session.GetInt32("userID"));

            using (SqlDataReader reader = DBProject.UserTaskReader(HttpContext.Session.GetInt32("userID")))
            {
                while (reader.Read())
                {
                    UserTaskList.Add(new Tasks
                    {
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        Objective = reader["Objective"].ToString(),
                        DueDate = Convert.ToDateTime(reader["DueDate"])
                    });
                }
            }
            DBProject.DBConnection.Close();

        }
    }
}
