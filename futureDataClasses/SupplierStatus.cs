namespace CAREapplication.Pages.Data_Classes
{
    public class SupplierStatus
    {
        public int SupplierStatusID { get; set; }

        public int SupplierID { get; set;}

        public String? StatusName { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}
