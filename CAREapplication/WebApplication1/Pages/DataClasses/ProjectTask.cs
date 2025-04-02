namespace CAREapplication.Pages.DataClasses
{
    public class ProjectTask
    {
        public int TaskID { get; set; }
        public int ProjectID { get; set; }
        public DateTime DueDate { get; set; }
        public String? Objective { get; set; }
        public int Completed { get; set; }

    }
}
