using System.ComponentModel.DataAnnotations;

namespace lab484.Pages.Data_Classes
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Home Address is required")]
        public string? HomeAddress { get; set; }
    }

    public class Employee : User
    {
        public bool? AdminStatus { get; set; }
    }

    public class BPrep : User
    {
        public string? CommunicationStatus { get; set; }
    }

    public class Faculty : User
    {
    }

    public class NonFaculty : User
    {
    }

    
}
