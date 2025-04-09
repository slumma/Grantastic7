using CAREapplication.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.DB
{
    public class DBAdmin
    {
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString =
            "Server=Localhost;Database=CARE;Trusted_Connection=True";

        //Methods
        public static SqlDataReader adminGrantReader()
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
                                            g.GrantStatus, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
                                        JOIN grantFunder s ON g.FunderID = s.FunderID
                                        LEFT JOIN project p ON g.ProjectID = p.ProjectID
                                        ORDER BY g.AwardDate";


            cmdGrantReader.Connection.Open();
            SqlDataReader tempReader = cmdGrantReader.ExecuteReader();
            return tempReader;
        }
    }
}
