namespace CAREapplication.Pages.DataClasses
{
    public class BusinessPartner
    {
        public int FunderID { get; set; }
        public String FunderName { get; set; }
        public String? OrgType { get; set; }
        public String? CommunicationStatus { get; set; }
        public int UserID { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Email { get; set; }
        public String? Phone { get; set; }
        public String? HomeAddress { get; set; }
        public String? FunderStatus { get; set; }
        public String? BusinessAddress { get; set; }
        public string? City { get; set; }            
        public string? HomeState { get; set; }        
        public string? Zip { get; set; }
        public int funderPOCID { get; set; }
    }
}
