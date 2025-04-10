using CAREapplication.Pages.DataClasses;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CAREapplication.Pages.DB
{
    public class DBProject
    {
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly System.String? DBConnString =
            "Server=Localhost;Database=CARE;Trusted_Connection=True";

        //Methods
        public static int managerCheck(int userID, int projectID)
        {
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.Connection = DBConnection;
            cmdCheck.Connection.ConnectionString = DBConnString;
            cmdCheck.CommandText = "SELECT Leader FROM projectStaff WHERE UserID = @UserID AND ProjectID = @ProjectID;";
            cmdCheck.Parameters.AddWithValue("@UserID", userID);
            cmdCheck.Parameters.AddWithValue("@ProjectID", projectID);
            cmdCheck.Connection.Open();
            int status = Convert.ToInt32(cmdCheck.ExecuteScalar());
            return status;
        }

        public static void AddProject(ProjectSimple project, List<int> assignedFacultyList)
        {
            string insertProjectQuery = "INSERT INTO dbo.project (ProjectName, DueDate) VALUES (@ProjectName, @DueDate); SELECT SCOPE_IDENTITY();";
            string insertProjectStaffQuery = "INSERT INTO dbo.projectStaff (ProjectID, UserID, Leader, Active) VALUES (@ProjectID, @UserID, @Leader, @Active)";

            // help from AI (copilot) to insert a project into the db if it was not already created

            using (SqlConnection connection = new SqlConnection(DBProject.DBConnString))
            {
                connection.Open();

                using (SqlCommand cmdProjectInsert = new SqlCommand(insertProjectQuery, connection))
                {
                    cmdProjectInsert.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    cmdProjectInsert.Parameters.AddWithValue("@DueDate", project.DueDate);

                    project.ProjectID = Convert.ToInt32(cmdProjectInsert.ExecuteScalar());
                }

                foreach (var userID in assignedFacultyList)
                {
                    using (SqlCommand cmdProjectStaffInsert = new SqlCommand(insertProjectStaffQuery, connection))
                    {
                        cmdProjectStaffInsert.Parameters.AddWithValue("@ProjectID", project.ProjectID);
                        cmdProjectStaffInsert.Parameters.AddWithValue("@UserID", userID);
                        cmdProjectStaffInsert.Parameters.AddWithValue("@Leader", 0);
                        cmdProjectStaffInsert.Parameters.AddWithValue("@Active", 1);

                        cmdProjectStaffInsert.ExecuteNonQuery();
                    }
                }
            }
        }
        public static void InsertProjectStaff(User u, int projectID)
        {

            SqlConnection connection = new SqlConnection(DBConnString);

            System.String sqlQuery = "INSERT INTO projectStaff (UserID, ProjectID, Leader, Active) VALUES(@UserID, @ProjectID, 0, 1);";
            SqlCommand cmdInsertProjectStaff = new SqlCommand(sqlQuery, connection);

            // Add parameters to the command
            cmdInsertProjectStaff.Parameters.AddWithValue("@UserID", u.UserID);
            cmdInsertProjectStaff.Parameters.AddWithValue("@ProjectID", projectID);

            connection.Open();
            int rowsAffected = cmdInsertProjectStaff.ExecuteNonQuery();

            connection.Close();
        }
        public static SqlDataReader ProjectNoteReader(int ProjectID)
        {
            SqlCommand cmdViewNotes = new SqlCommand(DBConnString);
            cmdViewNotes.Connection = DBConnection;
            cmdViewNotes.Connection.ConnectionString = DBConnString;
            cmdViewNotes.CommandText = @"SELECT *
                                        FROM projectNotes pn
                                        JOIN users u ON pn.AuthorID = u.UserID
                                        JOIN person p ON u.UserID = p.UserID
                                        WHERE pn.ProjectID = @ProjectID;";

            cmdViewNotes.Parameters.AddWithValue("@ProjectID", ProjectID);

            cmdViewNotes.Connection.Open();

            SqlDataReader tempReader = cmdViewNotes.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader ProjectReader()
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = DBConnection;
            cmdProjectRead.Connection.ConnectionString = DBConnString;
            cmdProjectRead.CommandText = "SELECT project.ProjectID, project.ProjectDescription, project.ProjectName, project.DueDate, sum(grants.amount) AS Amount\r\nfrom project\r\nLEFT JOIN grants on project.ProjectID = grants.ProjectID\r\ngroup by project.ProjectID, project.ProjectName, project.duedate, project.ProjectDescription;";
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }


        public static SqlDataReader UserProjectReader(int userID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = DBConnection;
            cmdProjectRead.Connection.ConnectionString = DBConnString;
            cmdProjectRead.CommandText = @"SELECT 
                                            p.ProjectID, 
                                            p.ProjectName, 
                                            p.ProjectDescription, 
                                            p.DueDate, 
                                            SUM(g.Amount) AS Amount
                                        FROM 
                                            project p
                                        JOIN 
                                            projectStaff ps ON p.ProjectID = ps.ProjectID
                                        LEFT JOIN 
                                            grants g ON p.ProjectID = g.ProjectID
                                        WHERE 
                                            ps.UserID = @UserID
                                        GROUP BY 
                                            p.ProjectID, p.ProjectName, p.ProjectDescription, p.DueDate;";
            cmdProjectRead.Parameters.AddWithValue("@UserID", userID);
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }

        public static List<int> ProjectProgress(int ProjectID)
        {
            List<int> progressList = new List<int>();

            string queryCompleted = "SELECT COUNT(*) FROM projectTask WHERE ProjectID = @ProjectID AND Completed = 1;";
            string queryTotal = "SELECT COUNT(*) FROM projectTask WHERE ProjectID = @ProjectID;";

            using (SqlConnection connection = new SqlConnection(DBConnString))
            {
                connection.Open();

                using (SqlCommand cmdProgressRead = new SqlCommand(queryCompleted, connection))
                {
                    cmdProgressRead.Parameters.AddWithValue("@ProjectID", ProjectID);
                    int progress = Convert.ToInt32(cmdProgressRead.ExecuteScalar());
                    progressList.Add(progress);
                }

                using (SqlCommand cmdTotalRead = new SqlCommand(queryTotal, connection))
                {
                    cmdTotalRead.Parameters.AddWithValue("@ProjectID", ProjectID);
                    int total = Convert.ToInt32(cmdTotalRead.ExecuteScalar());
                    progressList.Add(total);
                }
            }

            return progressList;
        }

        public static SqlDataReader projectStaffReader(int ProjectID)
        {
            SqlCommand cmdProjectStaffReader = new SqlCommand();
            cmdProjectStaffReader.Connection = DBConnection;
            cmdProjectStaffReader.Connection.ConnectionString = DBConnString;
            cmdProjectStaffReader.CommandText = @"SELECT DISTINCT
                                                    u.UserID,
                                                    u.Username,
                                                    p.FirstName,
                                                    p.LastName,
                                                    c.Email,
                                                    c.Phone,
                                                    c.HomeAddress,
                                                    ps.ProjectID,
                                                    pr.ProjectName,
                                                    ps.Leader,
                                                    ps.Active
                                                FROM 
                                                    projectStaff ps
                                                JOIN 
                                                    users u ON ps.UserID = u.UserID
                                                JOIN 
                                                    person p ON u.UserID = p.UserID
                                                JOIN 
                                                    contact c ON p.PersonID = c.PersonID
                                                JOIN 
                                                    project pr ON ps.ProjectID = pr.ProjectID
                                                WHERE 
                                                    ps.ProjectID = @ProjectID AND 
                                                    ps.Active = 1;";

            cmdProjectStaffReader.Parameters.AddWithValue("@ProjectID", ProjectID);

            cmdProjectStaffReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdProjectStaffReader.ExecuteReader();

            return tempReader;
        }
        public static SqlDataReader singleProjectReader(int ProjectID)
        {
            if (DBConnection.State != ConnectionState.Closed)
            {
                DBConnection.Close();
            }

            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = DBConnection;
            cmdProjectRead.Connection.ConnectionString = DBConnString;
            cmdProjectRead.CommandText = @"	SELECT 
                                            project.ProjectID, 
                                            project.ProjectName, 
                                            project.ProjectDescription, 
                                            project.DueDate, 
                                            SUM(grants.Amount) AS Amount,
                                            STRING_AGG(grants.GrantName, ', ') WITHIN GROUP (ORDER BY grants.GrantName) AS GrantNames
                                        FROM project
                                        LEFT JOIN grants ON project.ProjectID = grants.ProjectID
                                        WHERE project.ProjectID = @ProjectID
                                        GROUP BY 
                                            project.ProjectID, 
                                            project.ProjectName, 
                                            project.ProjectDescription, 
                                            project.DueDate;";
            cmdProjectRead.Parameters.AddWithValue("@ProjectID", ProjectID);
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader projectTaskReader(int projectID)
        {
            SqlCommand cmdTaskRead = new SqlCommand();
            cmdTaskRead.Connection = DBConnection;
            cmdTaskRead.Connection.ConnectionString = DBConnString;

            cmdTaskRead.CommandText = "SELECT * from ProjectTask WHERE ProjectID = @ProjectID";
            cmdTaskRead.Parameters.AddWithValue("@ProjectID", projectID);
            cmdTaskRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader UserTaskReader(int? UserID)
        {
            SqlCommand cmdTaskRead = new SqlCommand();
            cmdTaskRead.Connection = DBConnection;
            cmdTaskRead.Connection.ConnectionString = DBConnString;

            cmdTaskRead.CommandText = @"SELECT 
                                            gt.TaskID, 
                                            g.GrantID AS RelatedEntityID, 
                                            g.GrantName AS RelatedEntityName, 
                                            gt.DueDate, 
                                            gt.Objective, 
                                            'Grant Task' AS TaskType
                                        FROM grantTaskStaff gts
                                        JOIN grantTask gt ON gts.TaskID = gt.TaskID
                                        JOIN grants g ON gt.GrantID = g.GrantID
                                        WHERE gts.AssigneeID = @UserID  AND gt.Completed = 0

                                        UNION ALL

                                        SELECT 
                                            pt.TaskID, 
                                            p.ProjectID AS RelatedEntityID, 
                                            p.ProjectName AS RelatedEntityName, 
                                            pt.DueDate, 
                                            pt.Objective, 
                                            'Project Task' AS TaskType
                                        FROM projectTaskStaff pts
                                        JOIN projectTask pt ON pts.TaskID = pt.TaskID
                                        JOIN project p ON pt.ProjectID = p.ProjectID
                                        WHERE pts.AssigneeID = @UserID AND pt.Completed = 0;";
            cmdTaskRead.Parameters.AddWithValue("@UserID", UserID);
            cmdTaskRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskRead.ExecuteReader();
            return tempReader;
        }

        


        public static SqlDataReader taskStaffReader(int projectID)
        {
            SqlCommand cmdTaskStaffRead = new SqlCommand();
            cmdTaskStaffRead.Connection = DBConnection;
            cmdTaskStaffRead.Connection.ConnectionString = DBConnString;

            cmdTaskStaffRead.CommandText = @"SELECT *
                                            FROM projectTaskStaff pts
                                            JOIN projectTask pt ON pt.TaskID = pts.TaskID
                                            JOIN users u ON pts.AssigneeID = u.UserID
                                            JOIN person p ON u.UserID = p.UserID
                                            WHERE pt.ProjectID = @ProjectID;";
            cmdTaskStaffRead.Parameters.AddWithValue("@ProjectID", projectID);
            cmdTaskStaffRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskStaffRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader projectSearch(string searchTerm)
        {
            SqlCommand cmdProjectSearch = new SqlCommand();
            cmdProjectSearch.Connection = DBConnection;
            cmdProjectSearch.Connection.ConnectionString = DBConnString;

            cmdProjectSearch.CommandText = @"SELECT 
                                                project.ProjectID, 
                                                project.ProjectDescription, 
                                                project.ProjectName, 
                                                project.DueDate, 
                                                SUM(grants.amount) AS Amount
                                            FROM project
                                            LEFT JOIN grants ON project.ProjectID = grants.ProjectID
                                            WHERE project.ProjectName LIKE '%' + @SearchTerm + '%'
                                            GROUP BY project.ProjectID, project.ProjectName, project.DueDate, project.ProjectDescription;";

            cmdProjectSearch.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmdProjectSearch.Connection.Open();
            SqlDataReader tempReader = cmdProjectSearch.ExecuteReader();

            return tempReader;

        }
        public static void InsertProjectNote(int projectID, string content, int userID)
        {
            string query = @"INSERT INTO ProjectNotes (ProjectID, Content, AuthorID, DateAdded)
                     VALUES (@ProjectID, @Content, @AuthorID, GETDATE())";

            SqlCommand cmd = new SqlCommand(query, DBConnection);
            cmd.Parameters.AddWithValue("@ProjectID", projectID);
            cmd.Parameters.AddWithValue("@Content", content);
            cmd.Parameters.AddWithValue("@AuthorID", userID);

            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();
        }



        public static int InsertProjectTask(int projectID, string objective, DateOnly dueDate)
        {
            string query = @"INSERT INTO projectTask (ProjectID, Objective, DueDate) 
                      VALUES (@ProjectID, @Objective, @DueDate);
                      SELECT CAST(scope_identity() AS int);";

            SqlCommand cmd = new SqlCommand(query, DBConnection);
            cmd.Parameters.AddWithValue("@ProjectID", projectID);
            cmd.Parameters.AddWithValue("@Objective", objective);
            cmd.Parameters.AddWithValue("@DueDate", dueDate);

            DBConnection.Open();
            int newTaskID = (int)cmd.ExecuteScalar();
            DBConnection.Close();

            return newTaskID;
        }



        public static void UpdateProjectTask(int taskID, int completedFlag)
        {
            string query = @"
                    UPDATE projectTask
                    SET 
                        Completed = @Completed
                    WHERE TaskID = @TaskID;
                ";
            SqlCommand cmd = new SqlCommand(query, DBConnection);

            cmd.Parameters.AddWithValue("@Completed", completedFlag); 
            cmd.Parameters.AddWithValue("@TaskID", taskID);
            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();
                
            }

        public static void UpdateProject(int ProjectID, string description, DateOnly duedate)
        {
            Trace.WriteLine(description);

            string query = @"
                        UPDATE project
                        SET
                            ProjectDescription = @description,
                            DueDate = @duedate
                        WHERE ProjectID = @projectID;";
            SqlCommand cmd = new SqlCommand(query, DBConnection);

            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@duedate", duedate.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@projectID", ProjectID);

            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();

        }

        public static void inactiveProjectStaff(int userID, int projectID)
        {

            string query = @"UPDATE projectStaff
                            SET
                                Active = 0
                            WHERE UserID = @userID AND ProjectID = @projectID;";
            SqlCommand cmd = new SqlCommand(query, DBConnection);

            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@projectID", projectID);

            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();

        }

        public static void InsertProjectStaff(int projectID, int userID)
        {
            string query = @" INSERT INTO projectStaff(ProjectID, UserID, Leader, Active)
                                VALUES(@ProjectID, @UserID, 0, 1);";

            SqlCommand cmd = new SqlCommand(query, DBConnection);
            cmd.Parameters.AddWithValue("@ProjectID", projectID);
            cmd.Parameters.AddWithValue("@UserID", userID);

            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();
        }

        public static void InsertProjectTaskAndAssignToAllStaff(int projectID, string objective, DateOnly dueDate, int assignerID)
        {
            string insertTaskQuery = @"
        INSERT INTO projectTask (ProjectID, Objective, DueDate)
        VALUES (@ProjectID, @Objective, @DueDate);
        SELECT CAST(SCOPE_IDENTITY() AS INT);
    ";

            SqlCommand cmd = new SqlCommand(insertTaskQuery, DBConnection);
            cmd.Parameters.AddWithValue("@ProjectID", projectID);
            cmd.Parameters.AddWithValue("@Objective", objective);
            cmd.Parameters.AddWithValue("@DueDate", dueDate.ToDateTime(TimeOnly.MinValue));

            DBConnection.Open();
            int newTaskID = (int)cmd.ExecuteScalar();
            DBConnection.Close();

            // Get active project staff
            string getStaffQuery = "SELECT UserID FROM projectStaff WHERE ProjectID = @ProjectID AND Active = 1";
            SqlCommand getStaffCmd = new SqlCommand(getStaffQuery, DBConnection);
            getStaffCmd.Parameters.AddWithValue("@ProjectID", projectID);

            List<int> userIDs = new List<int>();
            DBConnection.Open();
            SqlDataReader reader = getStaffCmd.ExecuteReader();
            while (reader.Read())
            {
                userIDs.Add(Convert.ToInt32(reader["UserID"]));
            }
            reader.Close();
            DBConnection.Close();

            // Insert into projectTaskStaff for each user
            foreach (int userID in userIDs)
            {
                SqlCommand insertStaffCmd = new SqlCommand(@"
            INSERT INTO projectTaskStaff (TaskID, AssigneeID, AssignerID, DueDate)
            VALUES (@TaskID, @AssigneeID, @AssignerID, @DueDate)
        ", DBConnection);

                insertStaffCmd.Parameters.AddWithValue("@TaskID", newTaskID);
                insertStaffCmd.Parameters.AddWithValue("@AssigneeID", userID);
                insertStaffCmd.Parameters.AddWithValue("@AssignerID", assignerID);
                insertStaffCmd.Parameters.AddWithValue("@DueDate", dueDate.ToDateTime(TimeOnly.MinValue));

                DBConnection.Open();
                insertStaffCmd.ExecuteNonQuery();
                DBConnection.Close();
            }
        }


    }
}

