namespace CAREapplication.Pages.Data_Classes
{
    public class Note
    {
        public int NoteID { get; set; }

        public int ProjectID { get; set; }

        public String? NoteContents { get; set; }

        public DateTime DatePosted { get; set; }
    }
}
