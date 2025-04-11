namespace CAREapplication.Pages.DataClasses
{
    public class GrantStaff
    {
        public int GrantStaffID { get; set; }
        public int GrantID { get; set; }
        public string GrantName { get; set; }
        public string UserRole {  get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HomeAddress { get; set; }
        public int PrincipalInvestigator { get; set; }
        public int CoPI { get; set; }
    }
}
