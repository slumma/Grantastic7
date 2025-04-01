namespace CAREapplication.Pages.DataClasses
{
    public class ProjectNote
    {
        public int ProjectID { get; set; }
        public string Content { get; set; }
        public DateTime TimeAdded { get; set; }
        public int AuthorID { get; set; }
        public string AuthorLast { get; set; }
        public string AuthorFirst { get; set; }
    }
}
