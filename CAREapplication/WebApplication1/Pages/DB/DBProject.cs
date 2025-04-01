using CAREapplication.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAREapplication.Pages.DB
{
    public class DBProject
    {
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString =
            "Server=Localhost;Database=Lab3;Trusted_Connection=True";

        //Methods
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
        public static void InsertProjectNote(ProjectNote newNote)
        {
            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO projectNotes(ProjectID, Content, AuthorID) VALUES (@GrantID, @Content, @AuthorID);";
            SqlCommand cmdInsertGrantNote = new SqlCommand(sqlQuery, connection);

            cmdInsertGrantNote.Parameters.AddWithValue("@GrantID", newNote.ProjectID);
            cmdInsertGrantNote.Parameters.AddWithValue("@Content", newNote.Content);
            cmdInsertGrantNote.Parameters.AddWithValue("@AuthorID", newNote.AuthorID);

            connection.Open();
            cmdInsertGrantNote.ExecuteNonQuery();
            connection.Close();
        }
        public static void InsertProjectStaff(User u, int projectID)
        {

            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO projectStaff (UserID, ProjectID, Leader, Active) VALUES(@UserID, @ProjectID, 0, 1);";
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
            cmdViewNotes.CommandText = @"SELECT * FROM projectNotes JOIN users ON projectNotes.AuthorID = users.UserID WHERE projectID = @ProjectID;";

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

        public static SqlDataReader projectStaffReader(int ProjectID)
        {
            SqlCommand cmdProjectStaffReader = new SqlCommand();
            cmdProjectStaffReader.Connection = DBConnection;
            cmdProjectStaffReader.Connection.ConnectionString = DBConnString;
            cmdProjectStaffReader.CommandText = @"SELECT DISTINCT
                                            u.UserID,
                                            u.Username,
                                            u.FirstName,
                                            u.LastName,
                                            u.Email,
                                            u.Phone,
                                            u.HomeAddress,
                                            ps.ProjectID,
                                            p.ProjectName,
                                            ps.Leader,
                                            ps.Active
                                        FROM 
                                            projectStaff ps
                                        JOIN 
                                            users u ON ps.UserID = u.UserID
                                        JOIN 
                                            project p ON ps.ProjectID = p.ProjectID
                                        WHERE ps.ProjectID = @ProjectID AND FacultyStatus = 1;";

            cmdProjectStaffReader.Parameters.AddWithValue("@ProjectID", ProjectID);

            cmdProjectStaffReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdProjectStaffReader.ExecuteReader();

            return tempReader;
        }
        public static SqlDataReader singleProjectReader(int ProjectID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = DBConnection;
            cmdProjectRead.Connection.ConnectionString = DBConnString;
            cmdProjectRead.CommandText = @"SELECT 
                                                project.ProjectID, 
                                                project.ProjectName, 
                                                project.ProjectDescription, 
                                                project.DueDate, 
                                                SUM(grants.Amount) AS Amount
                                            FROM project
                                            LEFT JOIN grants ON project.ProjectID = grants.ProjectID
                                            WHERE project.ProjectID = @ProjectID
                                            GROUP BY 
                                                project.ProjectID, 
                                                project.ProjectName, 
                                                project.ProjectDescription, 
                                                project.DueDate; ";
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
                                        WHERE gts.AssigneeID = @UserID

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
                                        WHERE pts.AssigneeID = @UserID;";
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

            cmdTaskStaffRead.CommandText = "SELECT * from projectTaskStaff\r\njoin projectTask on projectTask.taskid = projecttaskstaff.taskid\r\n" +
                "join users on projecttaskstaff.assigneeID = users.UserID\r\nWHERE ProjectID = @ProjectID";
            cmdTaskStaffRead.Parameters.AddWithValue("@ProjectID", projectID);
            cmdTaskStaffRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskStaffRead.ExecuteReader();
            return tempReader;
        }
    }
}
