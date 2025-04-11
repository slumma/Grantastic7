using CAREapplication.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.DB
{
    public class DBGrant
    {
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString =
            "Server=Localhost;Database=CARE;Trusted_Connection=True";

        /*
         * GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        Funder = grantReader["FunderName"].ToString(),
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["StatusName"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
         */
        public static SqlDataReader allGrantReader()
        {
            SqlCommand cmdGrantReader = new SqlCommand();
            cmdGrantReader.Connection = DBConnection;
            cmdGrantReader.Connection.ConnectionString = DBConnString;
            cmdGrantReader.CommandText = @"SELECT 
                                            g.GrantID, 
                                            g.GrantName,
                                            p.ProjectID,
                                            s.FunderName, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            gs.StatusName, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
                                        JOIN grantStatus gs ON g.GrantID = gs.GrantID
                                        JOIN Funder s ON g.FunderID = s.FunderID
                                        LEFT JOIN project p ON g.ProjectID = p.ProjectID
                                        ORDER BY g.AwardDate";


            cmdGrantReader.Connection.Open();
            SqlDataReader tempReader = cmdGrantReader.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader adminGrantReader()
        {
            SqlCommand cmdGrantReader = new SqlCommand();
            cmdGrantReader.Connection = DBConnection;
            cmdGrantReader.Connection.ConnectionString = DBConnString;

            cmdGrantReader.CommandText = @"
        SELECT 
            g.GrantID, 
            g.GrantName,
            p.ProjectID,
            s.FunderName AS Funder, 
            p.ProjectName AS Project, 
            g.Amount,
            g.Category,
            gs.StatusName, 
            g.descriptions,
            g.SubmissionDate, 
            g.AwardDate
        FROM grants g
        JOIN Funder s ON g.FunderID = s.FunderID
        LEFT JOIN project p ON g.ProjectID = p.ProjectID
        LEFT JOIN (
            SELECT GrantID, StatusName
            FROM (
                SELECT *,
                       ROW_NUMBER() OVER (PARTITION BY GrantID ORDER BY ChangeDate DESC) AS rn
                FROM grantStatus
            ) latest
            WHERE rn = 1
        ) gs ON g.GrantID = gs.GrantID
        ORDER BY g.AwardDate;
    ";

            cmdGrantReader.Connection.Open();
            SqlDataReader tempReader = cmdGrantReader.ExecuteReader();
            return tempReader;
        }


        public static SqlDataReader facGrantReader(int currentUserID)
        {
            SqlCommand cmdGrantReader = new SqlCommand();
            cmdGrantReader.Connection = DBConnection;
            cmdGrantReader.Connection.ConnectionString = DBConnString;

            cmdGrantReader.CommandText = @"
        SELECT 
            g.GrantID, 
            g.GrantName,
            p.ProjectID,
            s.FunderName AS Funder, 
            p.ProjectName AS Project, 
            g.Amount,
            g.Category,
            gs.StatusName AS GrantStatus, 
            g.descriptions,
            g.SubmissionDate, 
            g.AwardDate
        FROM grants g
        JOIN grantStaff gs2 ON g.GrantID = gs2.GrantID
        JOIN users u ON gs2.UserID = u.UserID
        JOIN Funder s ON g.FunderID = s.FunderID
        LEFT JOIN project p ON g.ProjectID = p.ProjectID
        LEFT JOIN (
            SELECT GrantID, StatusName
            FROM (
                SELECT *,
                       ROW_NUMBER() OVER (PARTITION BY GrantID ORDER BY ChangeDate DESC) AS rn
                FROM grantStatus
            ) latest
            WHERE rn = 1
        ) gs ON g.GrantID = gs.GrantID
        WHERE u.UserID = @UserID
        ORDER BY g.AwardDate;
    ";

            cmdGrantReader.Parameters.AddWithValue("@UserID", currentUserID);
            cmdGrantReader.Connection.Open();
            SqlDataReader tempReader = cmdGrantReader.ExecuteReader();
            return tempReader;
        }


        public static void UpdateGrantTask(int taskID, int completedFlag)
        {
            string query = @"
                    UPDATE grantTask
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

        public static SqlDataReader GrantNoteReader(int GrantID)
        {
            SqlCommand cmdViewNotes = new SqlCommand(DBConnString);
            cmdViewNotes.Connection = DBConnection;
            cmdViewNotes.Connection.ConnectionString = DBConnString;
            cmdViewNotes.CommandText = @"SELECT * FROM grantNotes JOIN users ON grantNotes.AuthorID = users.UserID JOIN 
                                        person p on users.UserID = p.UserID WHERE GrantID = @GrantID;";

            cmdViewNotes.Parameters.AddWithValue("@GrantID", GrantID);

            cmdViewNotes.Connection.Open();

            SqlDataReader tempReader = cmdViewNotes.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader grantTaskReader(int grantID)
        {
            SqlCommand cmdTaskRead = new SqlCommand();
            cmdTaskRead.Connection = DBConnection;
            cmdTaskRead.Connection.ConnectionString = DBConnString;

            cmdTaskRead.CommandText = "SELECT * from GrantTask WHERE grantID = @grantID";
            cmdTaskRead.Parameters.AddWithValue("@grantID", grantID);
            cmdTaskRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader taskStaffReader(int grantID)
        {
            SqlCommand cmdTaskStaffRead = new SqlCommand();
            cmdTaskStaffRead.Connection = DBConnection;
            cmdTaskStaffRead.Connection.ConnectionString = DBConnString;

            cmdTaskStaffRead.CommandText = "SELECT * from grantTaskStaff\r\njoin grantTask on grantTask.taskid = granttaskstaff.taskid\r\n" +
                "join users on granttaskstaff.assigneeID = users.UserID  JOIN person p ON users.UserID = p.UserID   \r\nWHERE GrantID = @GrantID";
            cmdTaskStaffRead.Parameters.AddWithValue("@GrantID", grantID);
            cmdTaskStaffRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskStaffRead.ExecuteReader();
            return tempReader;
        }

        public static List<int> GrantProgress(int GrantID)
        {
            List<int> progressList = new List<int>();

            SqlCommand cmdProgressRead = new SqlCommand();
            cmdProgressRead.Connection = DBConnection;
            cmdProgressRead.Connection.ConnectionString = DBConnString;
            cmdProgressRead.CommandText = "SELECT count(*) FROM grantTask WHERE GrantID = @GrantID AND Completed = 1;";
            cmdProgressRead.Connection.Open();
            cmdProgressRead.Parameters.AddWithValue("@GrantID", GrantID);
            int progress = Convert.ToInt32(cmdProgressRead.ExecuteScalar());
            DBGrant.DBConnection.Close();

            SqlCommand cmdTotalRead = new SqlCommand();
            cmdTotalRead.Connection = DBConnection;
            cmdTotalRead.Connection.ConnectionString = DBConnString;
            cmdTotalRead.CommandText = "SELECT count(*) FROM grantTask WHERE GrantID = @GrantID;";
            cmdTotalRead.Connection.Open();
            cmdTotalRead.Parameters.AddWithValue("@GrantID", GrantID);
            int total = Convert.ToInt32(cmdTotalRead.ExecuteScalar());
            DBGrant.DBConnection.Close();

            progressList.Add(progress);
            progressList.Add(total);

            return progressList;
        }

        public static void InsertGrant(GrantSimple g, int FunderID, int projectID, int userID)
        {
            String insertGrantQuery = "INSERT INTO grants (FunderID, GrantName, ProjectID, Category, SubmissionDate, descriptions, AwardDate, Amount) " +
                              "VALUES (@FunderID, @GrantName, @ProjectID, @Category, @SubmissionDate, @Descriptions, @AwardDate, @Amount); SELECT SCOPE_IDENTITY();";
            String insertGrantStaffQuery = "INSERT INTO grantStaff (GrantID, UserID) VALUES (@GrantID, @UserID);";
            String insertGrantStatus = "INSERT INTO grantStatus (StatusName, GrantID) VALUES (@StatusName, @GrantID);";

            int GrantID;


            // used AI to help implement the grants into the DB without the grantStaff freaking out 
            using (SqlCommand cmdInsertGrant = new SqlCommand(insertGrantQuery, DBConnection))
            {
                cmdInsertGrant.Connection.ConnectionString = DBConnString;

                cmdInsertGrant.Parameters.AddWithValue("@FunderID", FunderID);
                cmdInsertGrant.Parameters.AddWithValue("@GrantID", g.GrantID);
                cmdInsertGrant.Parameters.AddWithValue("@GrantName", g.GrantName);
                cmdInsertGrant.Parameters.AddWithValue("@StatusName", g.Status);
                cmdInsertGrant.Parameters.AddWithValue("@ProjectID", projectID);
                cmdInsertGrant.Parameters.AddWithValue("@GrantStatus", g.Status);
                cmdInsertGrant.Parameters.AddWithValue("@Category", g.Category);
                cmdInsertGrant.Parameters.AddWithValue("@SubmissionDate", g.SubmissionDate);
                cmdInsertGrant.Parameters.AddWithValue("@Descriptions", g.Description);
                cmdInsertGrant.Parameters.AddWithValue("@AwardDate", g.AwardDate);
                cmdInsertGrant.Parameters.AddWithValue("@Amount", g.Amount);


                cmdInsertGrant.Connection.Open();
                GrantID = Convert.ToInt32(cmdInsertGrant.ExecuteScalar());
                cmdInsertGrant.Connection.Close();
            }
            using (SqlCommand cmdInsertGrantStaff = new SqlCommand(insertGrantStaffQuery, DBConnection))
            {
                cmdInsertGrantStaff.Parameters.AddWithValue("@GrantID", GrantID);
                cmdInsertGrantStaff.Parameters.AddWithValue("@UserID", userID);

                cmdInsertGrantStaff.Connection.Open();
                cmdInsertGrantStaff.ExecuteNonQuery();
                cmdInsertGrantStaff.Connection.Close();
            }
        }

        public static void InsertGrantStaff(User u, int grantID)
        {

            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO grantStaff (UserID, grantID) VALUES (@UserID, @ProjectID);";
            SqlCommand cmdInsertGrantStaff = new SqlCommand(sqlQuery, connection);

            // Add parameters to the command
            cmdInsertGrantStaff.Parameters.AddWithValue("@UserID", u.UserID);
            cmdInsertGrantStaff.Parameters.AddWithValue("@ProjectID", grantID);

            connection.Open();
            int rowsAffected = cmdInsertGrantStaff.ExecuteNonQuery();

            connection.Close();
        }
        public static void InsertGrantNote(GrantNote newNote)
        {
            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO grantNotes(GrantID, Content, AuthorID) VALUES (@GrantID, @Content, @AuthorID);";
            SqlCommand cmdInsertGrantNote = new SqlCommand(sqlQuery, connection);

            cmdInsertGrantNote.Parameters.AddWithValue("@GrantID", newNote.GrantID);
            cmdInsertGrantNote.Parameters.AddWithValue("@Content", newNote.Content);
            cmdInsertGrantNote.Parameters.AddWithValue("@AuthorID", newNote.AuthorID);

            connection.Open();
            cmdInsertGrantNote.ExecuteNonQuery();
            connection.Close();
        }

        public static SqlDataReader SingleGrantReader(int GrantID)
        {
            SqlCommand cmdSingleGrantRead = new SqlCommand();
            cmdSingleGrantRead.Connection = DBConnection;
            cmdSingleGrantRead.Connection.ConnectionString = DBConnString;

            cmdSingleGrantRead.CommandText = @"SELECT 
                                            g.GrantID,
                                            g.GrantName,
                                            p.ProjectID,
                                            s.FunderName AS Funder, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            gs.StatusName, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
										JOIN grantStatus gs ON g.GrantID = gs.GrantID
                                        JOIN Funder s ON g.FunderID = s.FunderID
                                        LEFT JOIN project p ON g.ProjectID = p.ProjectID
                                            WHERE g.GrantID = @GrantID;";



            cmdSingleGrantRead.Parameters.AddWithValue("@GrantID", GrantID);

            cmdSingleGrantRead.Connection.Open();
            SqlDataReader tempReader = cmdSingleGrantRead.ExecuteReader();
            return tempReader;
        }

        public static void UpdateGrant(GrantSimple grant)
        {

            // queries *parameterized*
            // checks relative information such as Funder to ensure its there 
            // query to insert a Funder if it is not already in db
            string checkFunderQuery = "SELECT FunderID FROM Funder WHERE FunderName = @Funder";
            string insertFunderQuery = "INSERT INTO Funder (FunderName) OUTPUT INSERTED.FunderID VALUES (@Funder)";
            string updateGrantQuery = "UPDATE grants SET " +
                                      "FunderID = @FunderID, " +
                                      "GrantName = @GrantName," +
                                      "Amount = @Amount, " +
                                      "Category = @Category, " +
                                      "GrantStatus = @GrantStatus, " +
                                      "descriptions = @Description, " +
                                      "SubmissionDate = @SubmissionDate, " +
                                      "AwardDate = @AwardDate " +
                                      "WHERE GrantID = @GrantID; ";

            // db connection
            using (SqlConnection connection = new SqlConnection(DBGrant.DBConnString))
            {
                connection.Open();

                // check if Funder already exists 
                var FunderID = new SqlCommand(checkFunderQuery, connection)
                {
                    Parameters = { new SqlParameter("@Funder", grant.Funder) }
                }.ExecuteScalar();


                // if Funder does NOT exist, insert it 
                if (FunderID == null)
                {
                    FunderID = new SqlCommand(insertFunderQuery, connection)
                    {
                        Parameters = { new SqlParameter("@Funder", grant.Funder) }
                    }.ExecuteScalar();
                }

                // insert parameters into the query 
                using (SqlCommand cmdGrantUpdate = new SqlCommand(updateGrantQuery, connection))
                {
                    cmdGrantUpdate.Parameters.AddWithValue("@FunderID", FunderID);
                    cmdGrantUpdate.Parameters.AddWithValue("@GrantName", grant.GrantName);
                    cmdGrantUpdate.Parameters.AddWithValue("@Amount", grant.Amount);
                    cmdGrantUpdate.Parameters.AddWithValue("@Category", grant.Category);
                    cmdGrantUpdate.Parameters.AddWithValue("@GrantStatus", grant.Status);
                    cmdGrantUpdate.Parameters.AddWithValue("@Description", grant.Description);
                    cmdGrantUpdate.Parameters.AddWithValue("@SubmissionDate", grant.SubmissionDate);
                    cmdGrantUpdate.Parameters.AddWithValue("@AwardDate", grant.AwardDate);
                    cmdGrantUpdate.Parameters.AddWithValue("@GrantID", grant.GrantID);

                    cmdGrantUpdate.ExecuteNonQuery();
                }
            }
        }


        public static SqlDataReader searchGrant(string searchTerm)
        {
            SqlCommand cmdGrantReader = new SqlCommand();
            cmdGrantReader.Connection = DBConnection;
            cmdGrantReader.Connection.ConnectionString = DBConnString;
            cmdGrantReader.CommandText = @"SELECT 
                                            g.GrantID, 
                                            g.GrantName,
                                            p.ProjectID,
                                            s.FunderName AS Funder, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            gs.StatusName, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
										JOIN grantStatus gs ON g.GrantID = gs.GrantID
                                        JOIN Funder s ON g.FunderID = s.FunderID
                                        LEFT JOIN project p ON g.ProjectID = p.ProjectID
                                        WHERE g.GrantName LIKE '%' + @SearchTerm + '%'
                                        ORDER BY g.AwardDate";

            cmdGrantReader.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmdGrantReader.Connection.Open();
            SqlDataReader tempReader = cmdGrantReader.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader UserGrantReader(int? UserID)
        {
            SqlCommand cmdUserGrantRead = new SqlCommand();
            cmdUserGrantRead.Connection = DBConnection;
            cmdUserGrantRead.Connection.ConnectionString = DBConnString;

            cmdUserGrantRead.CommandText = @"SELECT 
                                                g.GrantID,
                                                g.GrantName,
                                                gs.UserRole,
                                                g.Category,
                                                g.SubmissionDate,
                                                g.AwardDate,
                                                g.Amount,
                                                s.FunderName,
                                                gst.StatusName AS GrantStatus -- From grantStatus table
                                            FROM 
                                                grants g
                                            JOIN 
                                                grantStaff gs ON g.GrantID = gs.GrantID
                                            LEFT JOIN 
                                                funder s ON g.FunderID = s.FunderID
                                            LEFT JOIN 
                                                grantStatus gst ON g.GrantID = gst.GrantID
                                            WHERE 
                                                gs.UserID = @UserID;";

            cmdUserGrantRead.Parameters.AddWithValue("@UserID", UserID);

            cmdUserGrantRead.Connection.Open();
            SqlDataReader tempReader = cmdUserGrantRead.ExecuteReader();
            return tempReader;
        }
        public static void InsertGrantNote(int grantID, string content, int userID)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO GrantNotes (GrantID, Content, AuthorID, DateAdded) VALUES (@GrantID, @Content, @UserID, GETDATE())", DBConnection);
            cmd.Parameters.AddWithValue("@GrantID", grantID);
            cmd.Parameters.AddWithValue("@Content", content);
            cmd.Parameters.AddWithValue("@UserID", userID);

            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();
        }

        public static void InsertGrantFile(int grantID, string filePath, string fileType, string nameFile)
        {
            string query = @"INSERT INTO files (FilePath, FileType, NameFile)
                     VALUES (@FilePath, @FileType, @NameFile);
                     DECLARE @FileID INT = SCOPE_IDENTITY();
                     INSERT INTO grantFile (GrantID, FileID)
                     VALUES (@GrantID, @FileID);";

            SqlCommand cmd = new SqlCommand(query, DBConnection);
            cmd.Parameters.AddWithValue("@FilePath", filePath);
            cmd.Parameters.AddWithValue("@FileType", fileType);
            cmd.Parameters.AddWithValue("@NameFile", nameFile);
            cmd.Parameters.AddWithValue("@GrantID", grantID);

            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();
        }

        public static List<FileRecord> GetGrantFiles(int grantID)
        {
            List<FileRecord> files = new List<FileRecord>();
            string query = @"SELECT f.FileID, f.FilePath, f.FileType, f.NameFile
                     FROM grantFile gf
                     INNER JOIN files f ON gf.FileID = f.FileID
                     WHERE gf.GrantID = @GrantID";

            SqlCommand cmd = new SqlCommand(query, DBConnection);
            cmd.Parameters.AddWithValue("@GrantID", grantID);

            DBConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                files.Add(new FileRecord
                {
                    FileID = (int)reader["FileID"],
                    FilePath = reader["FilePath"].ToString(),
                    FileType = reader["FileType"].ToString(),
                    NameFile = reader["NameFile"].ToString()
                });
            }
            DBConnection.Close();
            return files;
        }

        public static void InsertGrantTaskAndAssignToAllStaff(int grantID, string objective, DateOnly dueDate, int assignerID)
        {
            string insertTaskQuery = @"
        INSERT INTO grantTask (GrantID, Objective, DueDate)
        VALUES (@GrantID, @Objective, @DueDate);
        SELECT CAST(SCOPE_IDENTITY() AS INT);
    ";

            SqlCommand cmd = new SqlCommand(insertTaskQuery, DBConnection);
            cmd.Parameters.AddWithValue("@GrantID", grantID);
            cmd.Parameters.AddWithValue("@Objective", objective);
            cmd.Parameters.AddWithValue("@DueDate", dueDate.ToDateTime(TimeOnly.MinValue));

            DBConnection.Open();
            int newTaskID = (int)cmd.ExecuteScalar();
            DBConnection.Close();

            // Get all staff associated with the grant
            string getStaffQuery = "SELECT UserID FROM grantStaff WHERE GrantID = @GrantID";
            SqlCommand getStaffCmd = new SqlCommand(getStaffQuery, DBConnection);
            getStaffCmd.Parameters.AddWithValue("@GrantID", grantID);

            List<int> userIDs = new List<int>();
            DBConnection.Open();
            SqlDataReader reader = getStaffCmd.ExecuteReader();
            while (reader.Read())
            {
                userIDs.Add(Convert.ToInt32(reader["UserID"]));
            }
            reader.Close();
            DBConnection.Close();

            // Assign each user to the new task
            foreach (int userID in userIDs)
            {
                SqlCommand insertStaffCmd = new SqlCommand(@"
            INSERT INTO grantTaskStaff (TaskID, AssigneeID, AssignerID, DueDate)
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

        public static void InsertGrantStaff(int grantID, int userID, string userRole)
        {
            string query = @"
        INSERT INTO grantStaff (GrantID, UserID, UserRole)
        VALUES (@GrantID, @UserID, @UserRole);
    ";

            SqlCommand cmd = new SqlCommand(query, DBClass.DBConnection);
            cmd.Parameters.AddWithValue("@GrantID", grantID);
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@UserRole", userRole);

            DBClass.DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBClass.DBConnection.Close();
        }

        public static void UpdateGrantOverview(int grantID, string description, string category, float amount)
        {
            string query = @"
        UPDATE grants
        SET descriptions = @Description,
            Category = @Category,
            Amount = @Amount
        WHERE GrantID = @GrantID;
    ";

            SqlCommand cmd = new SqlCommand(query, DBConnection);
            cmd.Parameters.AddWithValue("@Description", description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Category", category ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@GrantID", grantID);

            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();
        }

    }
}
