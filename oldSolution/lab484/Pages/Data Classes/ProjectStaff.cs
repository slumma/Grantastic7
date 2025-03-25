namespace lab484.Pages.Data_Classes
{
    public class ProjectStaff
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HomeAddress { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public bool Leader { get; set; }
        public bool Active { get; set; }
    }
}
