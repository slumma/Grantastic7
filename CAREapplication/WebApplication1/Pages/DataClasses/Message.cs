namespace CAREapplication.Pages.DataClasses
{
    public class Message
    {
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public string SenderUsername { get; set; }
        public int RecipientID { get; set; }
        public string RecipientUsername { get; set; }
        public string SubjectTitle { get; set; }
        public string Contents { get; set; }
        public DateTime SentTime { get; set; }
    }
}
