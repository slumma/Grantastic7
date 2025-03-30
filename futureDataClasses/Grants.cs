namespace CAREapplication.Pages.Data_Classes
{
    public class Grants
    {
        public int GrantID { get; set; }

        public String? GrantName { get; set; }

        public int SupplierID { get; set; }

        public int ProjectID { get; set;}

        public String? StatusName { get; set; }

        public String? Category { get; set; }

        public DateTime SubmissionDate { get; set; }

        public String? descriptions { get; set; }

        public DateTime AwardDate { get; set; }

        public float Amount { get; set; }
    }
}
