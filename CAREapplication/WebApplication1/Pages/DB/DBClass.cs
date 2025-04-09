using CAREapplication.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Formats.Asn1;

namespace CAREapplication.Pages.DB
{
    public class DBClass
    {
        // Use this class to define methods that make connecting to
        // and retrieving data from the DB easier.

        // Connection Object at Data Field Level
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString =
            "Server=Localhost;Database=CARE;Trusted_Connection=True";

        private static readonly String? AUTHConnString =
            "Server=Localhost;Database=AUTH;Trusted_Connection=True";

        //Connection Methods:

        //Basic Product Reader
        public static SqlDataReader UserReader()
        {
            SqlCommand cmdUserReader = new SqlCommand();
            cmdUserReader.Connection = DBConnection;
            cmdUserReader.Connection.ConnectionString = DBConnString;
            cmdUserReader.CommandText = "SELECT * FROM users ORDER BY Username";
            cmdUserReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdUserReader.ExecuteReader();

            return tempReader;
        }

        public static User GetUserByID(int? userID)
        {
            User user = null;
            String sqlQuery = "SELECT * FROM users join person p on p.UserID = users.UserID where users.UserID = @UserID;";

            SqlConnection connection = new SqlConnection(DBConnString);
            SqlCommand cmdGetUser = new SqlCommand(sqlQuery, connection);
            cmdGetUser.Parameters.AddWithValue("@UserID", userID);

            connection.Open();
            SqlDataReader reader = cmdGetUser.ExecuteReader();

            if (reader.Read())
            {
                user = new User
                {
                    UserID = Int32.Parse(reader["UserID"].ToString()),
                    UserName = reader["Username"].ToString(),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    HomeAddress = reader["HomeAddress"].ToString()
                };
            }

            reader.Close();
            connection.Close();

            return user;
        }
        public static SqlDataReader SingleUserReader(String username)
        {
            SqlCommand cmdsingleSenderReader = new SqlCommand();
            cmdsingleSenderReader.Connection = DBConnection;
            cmdsingleSenderReader.Connection.ConnectionString = DBConnString;
            cmdsingleSenderReader.CommandText = @"select * from users where username = @username;";

            cmdsingleSenderReader.Parameters.AddWithValue("@username", username);

            cmdsingleSenderReader.Connection.Open();

            SqlDataReader tempReader = cmdsingleSenderReader.ExecuteReader();

            return tempReader;
        }
        

        public static int LoginQuery(string query, string username, string password)
        {
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = DBConnection;
            cmdLogin.Connection.ConnectionString = DBConnString;
            cmdLogin.CommandText = query;
            cmdLogin.Parameters.AddWithValue("@Username", username);
            cmdLogin.Parameters.AddWithValue("@Password", password);

            cmdLogin.Connection.Open();
            int rowCount = (int)cmdLogin.ExecuteScalar();
            cmdLogin.Connection.Close();

            return rowCount;
        }

        public static int loggedInUser(string query, string username, string password)
        {
            SqlCommand cmdUserID = new SqlCommand(query, DBConnection);
            cmdUserID.Parameters.AddWithValue("@Username", username);
            cmdUserID.Parameters.AddWithValue("@Password", password);

            DBConnection.Open();
            int userID = Convert.ToInt32(cmdUserID.ExecuteScalar());
            DBConnection.Close();

            return userID;
        }
        public static int employeeCheck(int userID)
        {
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.Connection = DBConnection;
            cmdCheck.Connection.ConnectionString = DBConnString;
            cmdCheck.CommandText = "SELECT EmployeeStatus FROM users WHERE UserID = @UserID;";
            cmdCheck.Parameters.AddWithValue("@UserID", userID);
            cmdCheck.Connection.Open();
            int status = Convert.ToInt32(cmdCheck.ExecuteScalar());
            return status;

        }
        public static int directorCheck(int userID)
        {
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.Connection = DBConnection;
            cmdCheck.Connection.ConnectionString = DBConnString;
            cmdCheck.CommandText = "SELECT Director FROM users WHERE UserID = @UserID;";
            cmdCheck.Parameters.AddWithValue("@UserID", userID);
            cmdCheck.Connection.Open();
            int status = Convert.ToInt32(cmdCheck.ExecuteScalar());
            return status;
        }
        public static int nonFacultyCheck(int userID)
        {
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.Connection = DBConnection;
            cmdCheck.Connection.ConnectionString = DBConnString;
            cmdCheck.CommandText = "SELECT NonFacultyStatus FROM users WHERE UserID = @UserID;";
            cmdCheck.Parameters.AddWithValue("@UserID", userID);
            cmdCheck.Connection.Open();
            int status = Convert.ToInt32(cmdCheck.ExecuteScalar());
            return status;
        }

        public static string StoredProcedureLogin(string Username)
        {
            string correctHash = "";

            SqlCommand cmdProductRead = new SqlCommand();

            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = AUTHConnString;
            cmdProductRead.CommandType = System.Data.CommandType.StoredProcedure;

            cmdProductRead.Parameters.AddWithValue("@Username", Username);
            cmdProductRead.CommandText = "sp_Lab3Login";

            cmdProductRead.Connection.Open();

            SqlDataReader passReader = cmdProductRead.ExecuteReader();
            if (passReader.Read())
            {
                correctHash = passReader["Password"].ToString();
            }
            cmdProductRead.Connection.Close();
            return correctHash;
        }

        public static bool UserCheck(string username)
        {
            string userCheckQuery = "SELECT COUNT(*) FROM HashedCredentials WHERE username = @Username";
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = DBConnection;
            cmdLogin.Connection.ConnectionString = AUTHConnString;
            cmdLogin.CommandText = userCheckQuery;
            cmdLogin.Parameters.AddWithValue("@Username", username);

            cmdLogin.Connection.Open();
            int rowCount = (int)cmdLogin.ExecuteScalar();
            cmdLogin.Connection.Close();

            if (rowCount > 0) { return true; }
            else { return false; }
        }

        public static bool HashedLogin(string Username, string Password)
        {
            
            string correctHash = StoredProcedureLogin(Username);

            if (PasswordHash.ValidatePassword(Password, correctHash))
            {
                return true;
            }

            return false;
        }

        public static int HashedUserID(string Username)
        {
            SqlCommand cmdGetHashedUserID = new SqlCommand();

            cmdGetHashedUserID.Connection = DBConnection;
            cmdGetHashedUserID.Connection.ConnectionString = AUTHConnString;

            cmdGetHashedUserID.CommandText = "SELECT UserID FROM HashedCredentials WHERE Username = @Username";
            cmdGetHashedUserID.Parameters.AddWithValue("@Username", Username);

            cmdGetHashedUserID.Connection.Open();

            SqlDataReader hashReader = cmdGetHashedUserID.ExecuteReader();

            int UserID = 0;
            if (hashReader.Read())
            {
                UserID = Convert.ToInt32(hashReader["UserID"].ToString());
            }
            cmdGetHashedUserID.Connection.Close();
            return UserID;
        }

        public static void InsertUser(User user)
        {
            String insertToUsers = "INSERT INTO users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress) " +
                              "VALUES (@Username, @Password, @FirstName, @LastName, @Email, @Phone, @HomeAddress)";

            CreateHashedUser(user.UserName, user.Password);

            // helped with AI to generate the insertion queries 
            using (SqlCommand cmdInsertUser = new SqlCommand(insertToUsers, DBConnection))
            {
                cmdInsertUser.Connection.ConnectionString = DBConnString;

                cmdInsertUser.Parameters.AddWithValue("@Username", user.UserName);
                cmdInsertUser.Parameters.AddWithValue("@Password", user.Password);
                cmdInsertUser.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmdInsertUser.Parameters.AddWithValue("@LastName", user.LastName);
                cmdInsertUser.Parameters.AddWithValue("@Email", user.Email);
                cmdInsertUser.Parameters.AddWithValue("@Phone", user.Phone);
                cmdInsertUser.Parameters.AddWithValue("@HomeAddress", user.HomeAddress);

                cmdInsertUser.Connection.Open();
                cmdInsertUser.ExecuteNonQuery();
                cmdInsertUser.Connection.Close();
            }
        }

        public static void CreateHashedUser(string Username, string Password)
        {
            string loginQuery =
                "INSERT INTO HashedCredentials (Username,Password) values (@Username, @Password)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = DBConnection;
            cmdLogin.Connection.ConnectionString = AUTHConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", PasswordHash.HashPassword(Password));

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();

            cmdLogin.Connection.Close();
        }

        public static SqlDataReader SearchFunction(string searchWord)
        {
            string SearchQuery = @"
        SELECT 'Users' AS TableName, 'Username' AS ColumnName, Username AS FoundValue 
        FROM users 
        WHERE Username LIKE @searchWord
        UNION
        SELECT 'Users' AS TableName, 'FirstName' AS ColumnName, FirstName AS FoundValue 
        FROM users 
        WHERE FirstName LIKE @searchWord
        UNION
        SELECT 'Users' AS TableName, 'LastName' AS ColumnName, LastName AS FoundValue 
        FROM users 
        WHERE LastName LIKE @searchWord
        UNION
        SELECT 'Users' AS TableName, 'Email' AS ColumnName, Email AS FoundValue 
        FROM users 
        WHERE Email LIKE @searchWord
        UNION
        SELECT 'Users' AS TableName, 'Phone' AS ColumnName, Phone AS FoundValue 
        FROM users 
        WHERE Phone LIKE @searchWord
        UNION
        SELECT 'Users' AS TableName, 'HomeAddress' AS ColumnName, HomeAddress AS FoundValue 
        FROM users 
        WHERE HomeAddress LIKE @searchWord
        UNION
        SELECT 'Project' AS TableName, 'ProjectName' AS ColumnName, ProjectName AS FoundValue 
        FROM project 
        WHERE ProjectName LIKE @searchWord
        UNION
        SELECT 'Project' AS TableName, 'ProjectDescription' AS ColumnName, ProjectDescription AS FoundValue 
        FROM project 
        WHERE ProjectDescription LIKE @searchWord
        UNION
        SELECT 'ProjectTask' AS TableName, 'Objective' AS ColumnName, Objective AS FoundValue 
        FROM projectTask 
        WHERE Objective LIKE @searchWord
        UNION
        SELECT 'Meeting' AS TableName, 'Purpose' AS ColumnName, Purpose AS FoundValue 
        FROM meeting 
        WHERE Purpose LIKE @searchWord
        UNION
        SELECT 'MeetingMinutes' AS TableName, 'UserID' AS ColumnName, CAST(UserID AS NVARCHAR) AS FoundValue 
        FROM meetingMinutes 
        WHERE CAST(UserID AS NVARCHAR) LIKE @searchWord
        UNION
        SELECT 'ProjectNotes' AS TableName, 'Content' AS ColumnName, Content AS FoundValue 
        FROM projectNotes 
        WHERE Content LIKE @searchWord
        UNION
        SELECT 'GrantSupplier' AS TableName, 'SupplierName' AS ColumnName, SupplierName AS FoundValue 
        FROM grantSupplier 
        WHERE SupplierName LIKE @searchWord
        UNION
        SELECT 'Grants' AS TableName, 'GrantName' AS ColumnName, GrantName AS FoundValue 
        FROM grants 
        WHERE GrantName LIKE @searchWord
        UNION
        SELECT 'Grants' AS TableName, 'Descriptions' AS ColumnName, Descriptions AS FoundValue 
        FROM grants 
        WHERE Descriptions LIKE @searchWord
        UNION
        SELECT 'GrantTask' AS TableName, 'Objective' AS ColumnName, Objective AS FoundValue 
        FROM grantTask 
        WHERE Objective LIKE @searchWord
        UNION
        SELECT 'GrantNotes' AS TableName, 'Content' AS ColumnName, Content AS FoundValue 
        FROM grantNotes 
        WHERE Content LIKE @searchWord
        UNION
        SELECT 'UserMessage' AS TableName, 'SubjectTitle' AS ColumnName, SubjectTitle AS FoundValue 
        FROM userMessage 
        WHERE SubjectTitle LIKE @searchWord
        UNION
        SELECT 'UserMessage' AS TableName, 'Contents' AS ColumnName, Contents AS FoundValue 
        FROM userMessage 
        WHERE Contents LIKE @searchWord
        ORDER BY TableName;
    ";

            SqlCommand cmdSearch = new SqlCommand(SearchQuery);
            cmdSearch.Connection = DBConnection;
            cmdSearch.Connection.ConnectionString = DBConnString;

            // ✅ Concatenate % on the C# side instead of in SQL
            string wildcardSearch = "%" + searchWord + "%";
            cmdSearch.Parameters.AddWithValue("@searchWord", wildcardSearch);

            if (cmdSearch.Connection.State != ConnectionState.Open)
            {
                cmdSearch.Connection.Open();
            }

            SqlDataReader tempReader = cmdSearch.ExecuteReader();
            return tempReader;
        }


    }
}

