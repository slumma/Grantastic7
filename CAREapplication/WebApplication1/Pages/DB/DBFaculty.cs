using CAREapplication.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.DB
{
    public class DBFaculty
    {
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString =
            "Server=Localhost;Database=Lab3;Trusted_Connection=True";

        //Methods
        public static SqlDataReader facReader()
        {
            SqlCommand cmdFacReader = new SqlCommand();
            cmdFacReader.Connection = DBConnection;
            cmdFacReader.Connection.ConnectionString = DBConnString;
            cmdFacReader.CommandText = "SELECT u.* FROM users u WHERE FacultyStatus = 1;";
            cmdFacReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdFacReader.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader singleFacultyReader(int GrantID)
        {
            SqlCommand cmdFacultyReader = new SqlCommand();
            cmdFacultyReader.Connection = DBConnection;
            cmdFacultyReader.Connection.ConnectionString = DBConnString;
            cmdFacultyReader.CommandText = @"SELECT DISTINCT
                                            u.UserID,
                                            u.Username,
                                            u.FirstName,
                                            u.LastName,
                                            u.Email,
                                            u.Phone,
                                            u.HomeAddress,
                                            g.GrantName,
                                            gs.UserRole
                                        FROM 
                                            grantStaff gs
                                        JOIN 
                                            users u ON gs.UserID = u.UserID
                                        JOIN 
                                            grants g ON gs.GrantID = g.GrantID
                                        WHERE gs.GrantID = @GrantID;";

            cmdFacultyReader.Parameters.AddWithValue("@GrantID", GrantID);

            cmdFacultyReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdFacultyReader.ExecuteReader();

            return tempReader;
        }


        public static SqlDataReader singleProjectFacultyReader(int ProjectID)
        {
            SqlCommand cmdFacultyReader = new SqlCommand();
            cmdFacultyReader.Connection = DBConnection;
            cmdFacultyReader.Connection.ConnectionString = DBConnString;
            cmdFacultyReader.CommandText = @"SELECT DISTINCT
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
                                            WHERE 
                                                ps.ProjectID = @ProjectID;";

            cmdFacultyReader.Parameters.AddWithValue("@ProjectID", ProjectID);

            cmdFacultyReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdFacultyReader.ExecuteReader();

            return tempReader;
        }
    }
}
