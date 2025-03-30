using CAREapplication.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages.DB
{
    public class DBMessage
    {
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString =
            "Server=Localhost;Database=Lab3;Trusted_Connection=True";
        public static void InsertUserMessage(int? senderID, int recipientID, string subjectTitle, string contents)
        {
            String sqlQuery = "INSERT INTO UserMessage (SenderID, RecipientID, SubjectTitle, Contents, SentTime) " +
                              "VALUES (@SenderID, @RecipientID, @SubjectTitle, @Contents, GETDATE())";

            // helped with AI for insertion statemetns so it doesnt break
            using (SqlCommand cmdInsertUserMessage = new SqlCommand(sqlQuery, DBConnection))
            {
                cmdInsertUserMessage.Connection.ConnectionString = DBConnString;

                cmdInsertUserMessage.Parameters.AddWithValue("@SenderID", senderID);
                cmdInsertUserMessage.Parameters.AddWithValue("@RecipientID", recipientID);
                cmdInsertUserMessage.Parameters.AddWithValue("@SubjectTitle", subjectTitle);
                cmdInsertUserMessage.Parameters.AddWithValue("@Contents", contents);

                cmdInsertUserMessage.Connection.Open();
                cmdInsertUserMessage.ExecuteNonQuery();
                cmdInsertUserMessage.Connection.Close();
            }
        }
        public static SqlDataReader singleRecipientReader(int UserID)
        {
            SqlCommand cmdsingleSenderReader = new SqlCommand();
            cmdsingleSenderReader.Connection = DBConnection;
            cmdsingleSenderReader.Connection.ConnectionString = DBConnString;
            cmdsingleSenderReader.CommandText = @"SELECT 
                                                    userMessage.*,
                                                    sender.Username AS SenderUsername,
                                                    recipient.Username AS RecipientUsername
                                                FROM 
                                                    userMessage
                                                JOIN 
                                                    Users AS sender ON userMessage.SenderID = sender.UserID
                                                JOIN 
                                                    Users AS recipient ON userMessage.RecipientID = recipient.UserID
                                                WHERE 
                                                    userMessage.RecipientID = @UserID
                                                ORDER BY SentTime DESC;";

            cmdsingleSenderReader.Parameters.AddWithValue("@UserID", UserID);

            cmdsingleSenderReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdsingleSenderReader.ExecuteReader();

            return tempReader;
        }
        public static SqlDataReader singleSenderReader(int UserID)
        {
            SqlCommand cmdsingleSenderReader = new SqlCommand();
            cmdsingleSenderReader.Connection = DBConnection;
            cmdsingleSenderReader.Connection.ConnectionString = DBConnString;
            cmdsingleSenderReader.CommandText = @"SELECT 
                                                    userMessage.*,
                                                    sender.Username AS SenderUsername,
                                                    recipient.Username AS RecipientUsername
                                                FROM 
                                                    userMessage
                                                JOIN 
                                                    Users AS sender ON userMessage.SenderID = sender.UserID
                                                JOIN 
                                                    Users AS recipient ON userMessage.RecipientID = recipient.UserID
                                                WHERE 
                                                    userMessage.SenderID = @UserID;";

            cmdsingleSenderReader.Parameters.AddWithValue("@UserID", UserID);

            cmdsingleSenderReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdsingleSenderReader.ExecuteReader();

            return tempReader;
        }
    }
}