using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

namespace CAREapplication.Pages
{

    public class MessageConvoModel : PageModel
    {
        public required List<Message> receivedList { get; set; } = new List<Message>();
        public User otherUser { get; set; } = new User();
        public List<SelectListItem> Usernames { get; set; } = new List<SelectListItem>();
        [BindProperty]
        [Required(ErrorMessage = "Must include message content.")]
        public string MessageContent { get; set; }


        public IActionResult OnGet(int sender)
        {
            Usernames = new List<SelectListItem>();

            // Execute the userReader method from DBClass to load the usernames 
            using (SqlDataReader reader = DBClass.UserReader())
            {
                while (reader.Read())
                {
                    Usernames.Add(new SelectListItem
                    {
                        Value = reader["UserID"].ToString(),
                        Text = reader["Username"].ToString()
                    });
                }
                reader.Close();
                DBClass.DBConnection.Close();
            }

            Trace.WriteLine(sender); // works 

            otherUser = DBClass.GetUserByID(sender);

            LoadReceivedMessages(HttpContext.Session.GetInt32("userID"), otherUser.UserID);

            Trace.WriteLine(receivedList.Count);


    
            return Page();
        }

        public IActionResult OnPost(int sender)
        {
            Trace.WriteLine("Executed OnPost");

            Usernames = new List<SelectListItem>();

            // Execute the userReader method from DBClass to load the usernames 
            using (SqlDataReader reader = DBClass.UserReader())
            {
                while (reader.Read())
                {
                    Usernames.Add(new SelectListItem
                    {
                        Value = reader["UserID"].ToString(),
                        Text = reader["Username"].ToString()
                    });
                }
                reader.Close();
                DBClass.DBConnection.Close();
            }

            Trace.WriteLine(sender); // works 

            otherUser = DBClass.GetUserByID(sender);

            LoadReceivedMessages(HttpContext.Session.GetInt32("userID"), otherUser.UserID);

            Trace.WriteLine(receivedList.Count);

            return Page();
        }

        public IActionResult OnPostSendMessage(int sender)
        {
            Trace.WriteLine("Executed SendMessage");

            Usernames = new List<SelectListItem>();

            // Execute the userReader method from DBClass to load the usernames 
            using (SqlDataReader reader = DBClass.UserReader())
            {
                while (reader.Read())
                {
                    Usernames.Add(new SelectListItem
                    {
                        Value = reader["UserID"].ToString(),
                        Text = reader["Username"].ToString()
                    });
                }
                reader.Close();
                DBClass.DBConnection.Close();
            }

            Trace.WriteLine(sender); // works 

            otherUser = DBClass.GetUserByID(sender);

            LoadReceivedMessages(HttpContext.Session.GetInt32("userID"), otherUser.UserID);

            if (MessageContent != null)
            {
                DBMessage.InsertUserMessage(HttpContext.Session.GetInt32("userID"), otherUser.UserID, MessageContent);
            }

            ModelState.Clear();
            MessageContent = string.Empty;

            LoadReceivedMessages(HttpContext.Session.GetInt32("userID"), otherUser.UserID);

            Trace.WriteLine(receivedList.Count);

            return Page();
        }

        private void LoadReceivedMessages(int? recipient, int sender)
        {
            receivedList = new List<Message>();

            using (SqlDataReader receivedReader = DBMessage.singleConvoReader(recipient, sender))
            {
                while (receivedReader.Read())
                {
                    receivedList.Add(new Message
                    {
                        SenderID = int.Parse(receivedReader["SenderID"].ToString()),
                        SenderUsername = receivedReader["SenderUsername"].ToString(),
                        RecipientID = int.Parse(receivedReader["RecipientID"].ToString()),
                        RecipientUsername = receivedReader["RecipientUsername"].ToString(),
                        SubjectTitle = receivedReader["SubjectTitle"].ToString(),
                        Contents = receivedReader["Contents"].ToString(),
                        MessageID = int.Parse(receivedReader["MessageID"].ToString()),
                        SentTime = DateTime.Parse(receivedReader["SentTime"].ToString())
                    });
                }
                receivedReader.Close();
            }
            DBMessage.DBConnection.Close();
        }
    }
}