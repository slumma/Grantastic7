namespace CAREapplication.Pages.DataClasses
{
    public class BusinessPartner
    {
        public int SupplierID { get; set; }
        public String SupplierName { get; set; }
        public String OrgType { get; set; }
        public String? CommunicationStatus { get; set; }
        public int UserID { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Email { get; set; }
        public String? Phone { get; set; }
        public String? HomeAddress { get; set; }
        public String? SupplierStatus { get; set; }
    }
}
