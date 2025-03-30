using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages
{
    public class MessagesModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Must select a user to send message to.")]
        public int SelectedUsername { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Must include a subject title.")]
        public string MessageSubject { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Must include message content.")]
        public string MessageContent { get; set; }
        public List<SelectListItem> Usernames { get; set; } = new List<SelectListItem>();

        public User activeUser { get; set; }

        public string usr { get; set; } // Declare usr as a property of the class

        public required List<Message> receivedList { get; set; } = new List<Message>();
        public string SuccessMessage { get; set; }

        public IActionResult OnGet()
        {
            // Retrieve the username from the session here
            usr = HttpContext.Session.GetString("username");

            if (usr == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("Index"); // Redirect to login page
            }

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

            // Initialize activeUser to avoid NullReferenceException
            activeUser = new User();
            Trace.WriteLine("Initialized activeUser object");

            using (SqlDataReader singleUserReader = DBClass.SingleUserReader(usr))
            {
                if (singleUserReader != null && singleUserReader.HasRows)
                {
                    while (singleUserReader.Read())
                    {
                        Trace.WriteLine("Reading singleUserReader data");
                        activeUser.UserID = int.Parse(singleUserReader["UserID"].ToString());
                        activeUser.UserName = singleUserReader["Username"].ToString();
                        activeUser.FirstName = singleUserReader["FirstName"].ToString();
                        activeUser.LastName = singleUserReader["Lastname"].ToString();
                        activeUser.Email = singleUserReader["Email"].ToString();
                        Trace.WriteLine($"Active user details: {activeUser.UserID} {activeUser.UserName}, {activeUser.Email}");
                    }
                    singleUserReader.Close();

                    HttpContext.Session.SetInt32("ActiveUserID", activeUser.UserID);
                }
            }

            DBClass.DBConnection.Close();

            // Ensure receivedList is not null
            receivedList = new List<Message>();

            if (activeUser != null)
            {
                // Retrieve received messages
                LoadReceivedMessages(activeUser.UserID);
            }

            return Page();
        }

        

        // method specifically for loading the messages 
        private void LoadReceivedMessages(int userId)
        {
            receivedList = new List<Message>();

            using (SqlDataReader receivedReader = DBMessage.singleRecipientReader(userId))
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
