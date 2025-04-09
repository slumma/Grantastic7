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
        public static SqlDataReader allGrantReader()
        {
            SqlCommand cmdGrantReader = new SqlCommand();
            cmdGrantReader.Connection = DBConnection;
            cmdGrantReader.Connection.ConnectionString = DBConnString;
            cmdGrantReader.CommandText = @"SELECT 
                                            g.GrantID, 
                                            g.GrantName,
                                            p.ProjectID,
                                            s.SupplierName AS Supplier, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            g.GrantStatus, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
                                        JOIN grantSupplier s ON g.SupplierID = s.SupplierID
                                        LEFT JOIN project p ON g.ProjectID = p.ProjectID
                                        ORDER BY g.AwardDate";


            cmdGrantReader.Connection.Open();
            SqlDataReader tempReader = cmdGrantReader.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader facGrantReader(int currentUserID)
        {
            SqlCommand cmdGrantReader = new SqlCommand();
            cmdGrantReader.Connection = DBConnection;
            cmdGrantReader.Connection.ConnectionString = DBConnString;
            cmdGrantReader.CommandText = @"SELECT 
                                            g.GrantID, 
                                            g.GrantName,
                                            p.ProjectID,
                                            s.SupplierName AS Supplier, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            g.GrantStatus, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
                                        JOIN grantStaff ON g.grantID = grantstaff.grantID
                                        JOIN users ON grantstaff.userID = users.userID
                                        JOIN grantSupplier s ON g.SupplierID = s.SupplierID
                                        LEFT JOIN project p ON g.ProjectID = p.ProjectID
                                        WHERE users.userID = @UserID
                                        ORDER BY g.AwardDate";


            cmdGrantReader.Connection.Open();
            cmdGrantReader.Parameters.AddWithValue("@UserID", currentUserID);
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
            cmdViewNotes.CommandText = @"SELECT * FROM grantNotes JOIN users ON grantNotes.AuthorID = users.UserID WHERE GrantID = @GrantID;";

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
                "join users on granttaskstaff.assigneeID = users.UserID\r\nWHERE GrantID = @GrantID";
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

        public static void InsertGrant(GrantSimple g, int supplierID, int projectID, int userID)
        {
            String insertGrantQuery = "INSERT INTO grants (SupplierID, GrantName, ProjectID, GrantStatus, Category, SubmissionDate, descriptions, AwardDate, Amount) " +
                              "VALUES (@SupplierID, @GrantName, @ProjectID, @StatusName, @Category, @SubmissionDate, @Descriptions, @AwardDate, @Amount); SELECT SCOPE_IDENTITY();";
            String insertGrantStaffQuery = "INSERT INTO grantStaff (GrantID, UserID) VALUES (@GrantID, @UserID);";

            int GrantID;


            // used AI to help implement the grants into the DB without the grantStaff freaking out 
            using (SqlCommand cmdInsertGrant = new SqlCommand(insertGrantQuery, DBConnection))
            {
                cmdInsertGrant.Connection.ConnectionString = DBConnString;

                cmdInsertGrant.Parameters.AddWithValue("@SupplierID", supplierID);
                cmdInsertGrant.Parameters.AddWithValue("@GrantName", g.GrantName);
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
                                            s.SupplierName AS Supplier, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            g.GrantStatus, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
                                        JOIN grantSupplier s ON g.SupplierID = s.SupplierID
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
            // checks relative information such as supplier to ensure its there 
            // query to insert a supplier if it is not already in db
            string checkSupplierQuery = "SELECT SupplierID FROM grantSupplier WHERE SupplierName = @Supplier";
            string insertSupplierQuery = "INSERT INTO grantSupplier (SupplierName) OUTPUT INSERTED.SupplierID VALUES (@Supplier)";
            string updateGrantQuery = "UPDATE grants SET " +
                                      "SupplierID = @SupplierID, " +
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

                // check if supplier already exists 
                var supplierId = new SqlCommand(checkSupplierQuery, connection)
                {
                    Parameters = { new SqlParameter("@Supplier", grant.Supplier) }
                }.ExecuteScalar();


                // if supplier does NOT exist, insert it 
                if (supplierId == null)
                {
                    supplierId = new SqlCommand(insertSupplierQuery, connection)
                    {
                        Parameters = { new SqlParameter("@Supplier", grant.Supplier) }
                    }.ExecuteScalar();
                }

                // insert parameters into the query 
                using (SqlCommand cmdGrantUpdate = new SqlCommand(updateGrantQuery, connection))
                {
                    cmdGrantUpdate.Parameters.AddWithValue("@SupplierID", supplierId);
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
                                            s.SupplierName AS Supplier, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            g.GrantStatus, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
                                        JOIN grantSupplier s ON g.SupplierID = s.SupplierID
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
                                                g.StatusName,
                                                g.Category,
                                                g.SubmissionDate,
                                                g.AwardDate,
                                                g.Amount,
                                                g.GrantStatus,
                                                gs.UserRole,
                                                s.SupplierName
                                            FROM 
                                                grants g
                                            JOIN 
                                                grantStaff gs ON g.GrantID = gs.GrantID
                                            LEFT JOIN 
                                                grantSupplier s ON g.SupplierID = s.SupplierID
                                            WHERE 
                                                gs.UserID = @UserID;";

            cmdUserGrantRead.Parameters.AddWithValue("@UserID", UserID);

            cmdUserGrantRead.Connection.Open();
            SqlDataReader tempReader = cmdUserGrantRead.ExecuteReader();
            return tempReader;
        }
        public static void InsertGrantNote(int grantID, string content, int userID)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO GrantNotes (GrantID, Content, UserID, NoteDate) VALUES (@GrantID, @Content, @UserID, GETDATE())", DBConnection);
            cmd.Parameters.AddWithValue("@GrantID", grantID);
            cmd.Parameters.AddWithValue("@Content", content);
            cmd.Parameters.AddWithValue("@UserID", userID);

            DBConnection.Open();
            cmd.ExecuteNonQuery();
            DBConnection.Close();
        }
    }
}
