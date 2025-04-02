using Microsoft.VisualBasic;

namespace CAREapplication.Pages.DataClasses
{
    public class GrantTaskStaff
    {
        public int TaskStaffID { get; set; }
        public int TaskID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AssigneeID { get; set; }
        public int AssignerID { get; set; }
        public DateTime DueDate { get; set; }
    }
}