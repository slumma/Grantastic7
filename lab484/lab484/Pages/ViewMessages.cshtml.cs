using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace lab484.Pages
{
    public class ViewMessagesModel : PageModel
    {
        public int userID { get; set; }
        public required List<Message> sentList { get; set; } = new List<Message>();
        public required List<Message> receivedList { get; set; } = new List<Message>();

        public IActionResult OnGet(int userID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("Index"); // Redirect to login page
            }

            this.userID = userID;

            // Retrieve sent messages
            SqlDataReader sentReader = DBMessage.singleSenderReader(userID);
            while (sentReader.Read())
            {
                sentList.Add(new Message
                {
                    SenderID = Int32.Parse(sentReader["SenderID"].ToString()),
                    SenderUsername = sentReader["SenderUsername"].ToString(),
                    RecipientID = Int32.Parse(sentReader["RecipientID"].ToString()),
                    RecipientUsername = sentReader["RecipientUsername"].ToString(),
                    SubjectTitle = sentReader["SubjectTitle"].ToString(),
                    Contents = sentReader["Contents"].ToString(),
                    MessageID = Int32.Parse(sentReader["MessageID"].ToString()),
                    SentTime = DateTime.Parse(sentReader["SentTime"].ToString())
                });
            }
            DBClass.DBConnection.Close();

            // Retrieve received messages
            SqlDataReader receivedReader = DBMessage.singleRecipientReader(userID);
            while (receivedReader.Read())
            {
                receivedList.Add(new Message
                {
                    SenderID = Int32.Parse(receivedReader["SenderID"].ToString()),
                    SenderUsername = receivedReader["SenderUsername"].ToString(),
                    RecipientID = Int32.Parse(receivedReader["RecipientID"].ToString()),
                    RecipientUsername = receivedReader["RecipientUsername"].ToString(),
                    SubjectTitle = receivedReader["SubjectTitle"].ToString(),
                    Contents = receivedReader["Contents"].ToString(),
                    MessageID = Int32.Parse(receivedReader["MessageID"].ToString()),
                    SentTime = DateTime.Parse(receivedReader["SentTime"].ToString())
                });
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();
            return Page();
        }
    }
}
