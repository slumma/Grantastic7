namespace CAREapplication.Pages.Data_Classes
{
    public class Users
    {
        public int UserID { get; set; }

        public String? Username { get; set; }

        public String? Password { get; set; }

        public String? FirstName { get; set; }

        public String? LastName { get; set; }

        public String? Email { get; set; }

        public String? Phone { get; set; }

        public String? HomeAddress { get; set; }

        public String? PFPFilePath { get; set; }

        public int AdminStatus { get; set; }

        public int ViewArchives { get; set; }
    }
}
