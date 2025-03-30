namespace CAREapplication.Pages.Data_Classes
{
    public class ProjectTaskStaff
    {
        public int ProjectTaskStaffID { get; set; }

        public int ProjectTaskID { get; set; }

        public int AssigneeID { get; set; }

        public int AssignerID { get; set; }

        public DateTime DueDate { get; set; }
    }
}
