using CAREapplication.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.DB
{
    public class DBGrantSupplier
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
            cmdProductRead.CommandText = "SELECT grantSupplier.SupplierID, grantSupplier.SupplierName, grantSupplier.SupplierStatus, " +
            "OrgType, grantSupplier.BusinessAddress, bprep.UserID, CommunicationStatus, users.FirstName, " +
            "users.LastName, users.Email, users.Phone, users.HomeAddress " +
            "FROM grantSupplier " +
            "JOIN bprep ON grantSupplier.SupplierID = bprep.SupplierID " +
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
        public static SqlDataReader GrantSupplierReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM grantSupplier;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader SingleSupplierReader(int SupplierID)
        {
            SqlCommand cmdTaskStaffRead = new SqlCommand();
            cmdTaskStaffRead.Connection = DBConnection;
            cmdTaskStaffRead.Connection.ConnectionString = DBConnString;

            cmdTaskStaffRead.CommandText = @"SELECT * 
                                            FROM grantSupplier 
                                            JOIN BPrep b ON grantSupplier.SupplierID = b.SupplierID 
                                            JOIN users u on b.UserID = u.UserID
                                            WHERE grantSupplier.SupplierID = @SupplierID;";
            cmdTaskStaffRead.Parameters.AddWithValue("@SupplierID", SupplierID);
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
                                                join grantSupplier on grantSupplier.SupplierID = BPrep.SupplierID
                                                WHERE users.FirstName LIKE '%' + @SearchTerm + '%' 
                                                   OR users.LastName LIKE '%' + @SearchTerm + '%';";

            cmdProjectSearch.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmdProjectSearch.Connection.Open();
            SqlDataReader tempReader = cmdProjectSearch.ExecuteReader();

            return tempReader;

        }

    }
}
