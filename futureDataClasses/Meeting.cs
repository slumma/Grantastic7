namespace CAREapplication.Pages.Data_Classes
{
    public class Meeting
    {
        public int MeetingID { get; set; }

        public int ProjectID { get; set; }

        public DateTime MeetingDate { get; set; }

        public String? Purpose { get; set; }
    }
}
