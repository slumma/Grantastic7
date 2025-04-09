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
        public List<ProjectSimple> ProjectList { get; set; } = new List<ProjectSimple>();
        public List<GrantSimple> GrantList { get; set; } = new List<GrantSimple>();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("facultyStatus") != 1 && HttpContext.Session.GetInt32("director") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

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

            using (SqlDataReader reader = DBProject.UserProjectReader(HttpContext.Session.GetInt32("userID")))
            {
                while (reader.Read())
                {
                    ProjectList.Add(new ProjectSimple
                    {
                        ProjectID = Int32.Parse(reader["ProjectID"].ToString()),
                        ProjectName = reader["ProjectName"].ToString(),
                        DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                        Amount = reader["Amount"] != DBNull.Value && reader["Amount"].ToString() != "" ? float.Parse(reader["Amount"].ToString()) : 0f
                    });
                }
            }
            DBProject.DBConnection.Close();

            using (SqlDataReader grantReader = DBGrant.UserGrantReader(HttpContext.Session.GetInt32("userID")))
            {
                while (grantReader.Read())
                {
                    GrantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        Supplier = grantReader["SupplierName"].ToString(),
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            DBGrant.DBConnection.Close();

            return Page();
        }
    }
}
