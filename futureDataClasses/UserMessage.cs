namespace CAREapplication.Pages.Data_Classes
{
    public class UserMessage
    {
        public int MessageID { get; set; }

        public int SenderID { get; set; }

        public String? SubjectTitle { get; set; }

        public String? Contents { get; set; }

        public DateTime SentTime { get; set; }
    }
}
