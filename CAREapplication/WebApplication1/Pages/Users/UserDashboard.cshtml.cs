using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.Users
{
    public class UserDashboardModel : PageModel
    {
        public User activeUser { get; set; }

        public List<ProjectTask> ProjectTaskList { get; set; } = new List<ProjectTask>();
        public List<GrantTask> GrantTaskList { get; set; } = new List<GrantTask>();
        public List<ProjectSimple> ProjectList { get; set; } = new List<ProjectSimple>();
        public List<GrantSimple> GrantList { get; set; } = new List<GrantSimple>();
        public int activeUserID { get; set; } = new int();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            DBProject.DBConnection.Close();
            activeUser = DBClass.GetUserByID(HttpContext.Session.GetInt32("userID"));
            activeUserID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));

            //DIRECTOR VIEW
            if (Convert.ToInt32(HttpContext.Session.GetInt32("director")) == 1)
            {
                //Load Task List
                UserTaskReader(activeUserID);

                //Load Project List
                ProjectReader();

                //Load Grant List
                GrantReader();

                return Page();
            }

            //ADMIN ASSISTANT VIEW
            else if (Convert.ToInt32(HttpContext.Session.GetInt32("adminAsst")) == 1)
            {
                //Load Task List
                UserTaskReader(activeUserID);

                //Load Project List
                ProjectReader();

                //Load Grant List
                GrantReader(activeUserID);

                return Page();
            }

            //GENERAL USER VIEW
            else
            {
                //Load Task List
                UserTaskReader(activeUserID);

                //Load Project List
                UserProjectReader(activeUserID);
                Trace.WriteLine(ProjectList.Count());
                //Load Grant List
                UserGrantReader(activeUserID);

                return Page();
            }
        }

        public void UserTaskReader(int userID)
        {
            using (SqlDataReader reader = DBProject.UserTaskReader(HttpContext.Session.GetInt32("userID")))
            {
                while (reader.Read())
                {
                    if (reader["TaskType"].ToString() == "Project Task")
                    {
                        ProjectTaskList.Add(new ProjectTask
                        {
                            TaskID = Convert.ToInt32(reader["TaskID"]),
                            ProjectID = Convert.ToInt32(reader["RelatedEntityID"]),
                            Objective = reader["Objective"].ToString(),
                            DueDate = Convert.ToDateTime(reader["DueDate"])
                        });
                    }
                    else if (reader["TaskType"].ToString() == "Grant Task")
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

        public void ProjectReader()
        {
            using (SqlDataReader reader = DBProject.ProjectReader())
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
        }

        public void UserProjectReader(int userID)
        {
            using (SqlDataReader reader = DBProject.UserProjectReader(Convert.ToInt32(HttpContext.Session.GetInt32("userID"))))
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
        }
        public void GrantReader()
        {
            using (SqlDataReader grantReader = DBGrant.allGrantReader())
            {
                while (grantReader.Read())
                {
                    GrantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        Funder = grantReader["FunderName"].ToString(),
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["StatusName"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            DBGrant.DBConnection.Close();
        }

        public void GrantReader(int UserID)
        {
            using (SqlDataReader grantReader = DBGrant.facGrantReader(UserID))
            {
                while (grantReader.Read())
                {
                    GrantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        Funder = grantReader["FunderName"].ToString(),
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["StatusName"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            DBGrant.DBConnection.Close();
        }

        public void UserGrantReader(int userID)
        {
            using (SqlDataReader grantReader = DBGrant.UserGrantReader(HttpContext.Session.GetInt32("userID")))
            {
                while (grantReader.Read())
                {
                    GrantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        Funder = grantReader["FunderName"].ToString(),
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            DBGrant.DBConnection.Close();
        }
    }
}
