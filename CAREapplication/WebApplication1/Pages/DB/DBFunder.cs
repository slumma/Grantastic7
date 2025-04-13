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
        public static SqlDataReader FunderReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = @"SELECT 
                                            f.FunderID, 
                                            f.FunderName, 
                                            fs.StatusName, 
                                            f.OrgType, 
                                            f.BusinessAddress,


                                            fr.UserID AS RepUserID, 
                                            fr.CommunicationStatus AS RepCommStatus, 
                                            p.FirstName AS RepFirstName, 
                                            p.LastName AS RepLastName, 
                                            c.Email AS RepEmail, 
                                            c.Phone AS RepPhone, 
                                            c.HomeAddress AS RepAddress,


                                            poc.FirstName AS POCFirstName,
                                            poc.LastName AS POCLastName,
                                            contactPOC.Email AS POCEmail,
                                            contactPOC.Phone AS POCPhone,
                                            contactPOC.HomeAddress AS POCAddress,
                                            fp.CommunicationStatus AS POCCommStatus

                                        FROM funder f
                                        JOIN funderStatus fs ON f.FunderID = fs.FunderID
                                        JOIN funderRep fr ON f.FunderID = fr.FunderID
                                        JOIN users u ON fr.UserID = u.UserID
                                        JOIN person p ON u.UserID = p.UserID
                                        JOIN contact c ON p.PersonID = c.PersonID
                                        JOIN funderPOC fp ON f.FunderID = fp.FunderID
                                        JOIN person poc ON fp.PersonID = poc.PersonID
                                        JOIN contact contactPOC ON poc.PersonID = contactPOC.PersonID;
                                        ";
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
        public static SqlDataReader AllFunderReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM Funder;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader SingleFunderReader(int FunderID)
        {
            SqlCommand cmdTaskStaffRead = new SqlCommand();
            cmdTaskStaffRead.Connection = DBConnection;
            cmdTaskStaffRead.Connection.ConnectionString = DBConnString;

            cmdTaskStaffRead.CommandText = @"SELECT 
                                                f.FunderID,
                                                f.FunderName,
                                                f.OrgType,
                                                f.BusinessAddress,
                                                fpo.CommunicationStatus,
                                                fs.StatusName AS FunderStatus,
                                                p.FirstName,
                                                p.LastName,
                                                c.Email,
                                                c.Phone,
                                                c.HomeAddress,
	                                            c.City,
	                                            c.HomeState,
	                                            c.Zip
                                            FROM funder f
                                            JOIN funderPOC fpo ON f.FunderID = fpo.FunderID
                                            JOIN person p ON fpo.PersonID = p.PersonID
                                            JOIN contact c ON p.PersonID = c.PersonID
                                            LEFT JOIN (
                                                SELECT FunderID, StatusName
                                                FROM (
                                                    SELECT *,
                                                        ROW_NUMBER() OVER (PARTITION BY FunderID ORDER BY ChangeDate DESC) AS rn
                                                    FROM funderStatus
                                                ) fs
                                                WHERE fs.rn = 1
                                            ) fs ON f.FunderID = fs.FunderID
                                            WHERE f.FunderID = @FunderID;";
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
FROM funder f
JOIN funderRep fr ON f.FunderID = fr.FunderID
JOIN users u ON fr.UserID = u.UserID
JOIN person p ON u.UserID = p.UserID
JOIN contact c ON p.PersonID = c.PersonID
LEFT JOIN funderStatus fs ON f.FunderID = fs.FunderID
WHERE p.FirstName LIKE '%' + @SearchTerm + '%'
   OR p.LastName LIKE '%' + @SearchTerm + '%'
   OR f.FunderName LIKE '%' + @SearchTerm + '%';
";

            cmdProjectSearch.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmdProjectSearch.Connection.Open();
            SqlDataReader tempReader = cmdProjectSearch.ExecuteReader();

            return tempReader;

        }


    }
}
