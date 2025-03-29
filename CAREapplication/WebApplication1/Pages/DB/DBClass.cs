using Microsoft.Data.SqlClient;

namespace CAREapplication.Pages.DB
{
    public class DBClass
    {
        public static SqlConnection DBConnection = new SqlConnection();
        private static readonly String? DBConnString =
            "Server=Localhost;Database=CARE;Trusted_Connection=True";

        private static readonly String? AUTHConnString =
            "Server=Localhost;Database=AUTH;Trusted_Connection=True";


    }
}
