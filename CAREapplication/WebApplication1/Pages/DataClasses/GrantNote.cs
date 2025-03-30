namespace CAREapplication.Pages.DataClasses
{
    public class GrantNote
    {
        public int GrantID { get; set; }
        public string Content { get; set; }
        public DateTime TimeAdded { get; set; }
        public int AuthorID { get; set; }
        public string AuthorLast { get; set; }
        public string AuthorFirst { get; set; }
    }
}
