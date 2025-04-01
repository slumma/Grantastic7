namespace CAREapplication.Pages.Data_Classes
{
    public class GrantStatus
    {
        public int GrantStatusID { get; set; }

        public int GrantID { get; set; }

        public String? StatusName { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}
