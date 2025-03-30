namespace CAREapplication.Pages.Data_Classes
{
    public class ProjectTask
    {
        public int ProjectTaskID { get; set; }

        public int ProjectID { get; set; }

        public DateTime DueDate { get; set; }
        
        public String? Objective {  get; set; }
    }
}
