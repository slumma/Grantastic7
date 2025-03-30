using Microsoft.VisualBasic;

namespace lab484.Pages.Data_Classes
{
    public class TaskStaff
    {
        public int TaskStaffID { get; set; }
        public int TaskID { get; set; }
        public int AssigneeID { get; set; }
        public int AssignerID { get; set; }
        public DateTime DueDate { get; set; }
    }
}