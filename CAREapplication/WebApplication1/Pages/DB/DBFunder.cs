using CAREapplication.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.DB
{
    public class DBFunder
    {
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString =
            "Server=Localhost;Database=CARE;Trusted_Connection=True";

        //Methods
        public static SqlDataReader BPReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT grantFunder.FunderID, grantFunder.FunderName, grantFunder.FunderStatus, " +
            "OrgType, grantFunder.BusinessAddress, bprep.UserID, CommunicationStatus, users.FirstName, " +
            "users.LastName, users.Email, users.Phone, users.HomeAddress " +
            "FROM grantFunder " +
            "JOIN bprep ON grantFunder.FunderID = bprep.FunderID " +
            "JOIN users ON users.UserID = bprep.UserID;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader BPrepReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM BPrep\r\nJOIN users on users.userid = bprep.userid;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader GrantFunderReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM grantFunder;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader SingleFunderReader(int FunderID)
        {
            SqlCommand cmdTaskStaffRead = new SqlCommand();
            cmdTaskStaffRead.Connection = DBConnection;
            cmdTaskStaffRead.Connection.ConnectionString = DBConnString;

            cmdTaskStaffRead.CommandText = @"SELECT * 
                                            FROM grantFunder 
                                            JOIN BPrep b ON grantFunder.FunderID = b.FunderID 
                                            JOIN users u on b.UserID = u.UserID
                                            WHERE grantFunder.FunderID = @FunderID;";
            cmdTaskStaffRead.Parameters.AddWithValue("@FunderID", FunderID);
            cmdTaskStaffRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskStaffRead.ExecuteReader();
            return tempReader;
        }


        public static SqlDataReader BPSearch(string searchTerm)
        {
            SqlCommand cmdProjectSearch = new SqlCommand();
            cmdProjectSearch.Connection = DBConnection;
            cmdProjectSearch.Connection.ConnectionString = DBConnString;

            cmdProjectSearch.CommandText = @"SELECT * 
                                                FROM BPrep 
                                                JOIN users ON users.UserID = BPrep.UserID 
                                                join grantFunder on grantFunder.FunderID = BPrep.FunderID
                                                WHERE users.FirstName LIKE '%' + @SearchTerm + '%' 
                                                   OR users.LastName LIKE '%' + @SearchTerm + '%';";

            cmdProjectSearch.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmdProjectSearch.Connection.Open();
            SqlDataReader tempReader = cmdProjectSearch.ExecuteReader();

            return tempReader;

        }


    }
}
