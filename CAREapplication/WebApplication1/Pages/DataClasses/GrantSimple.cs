using System;
using System.ComponentModel.DataAnnotations;

namespace CAREapplication.Pages.DataClasses
{
    public class GrantSimple
    {
        public int GrantID { get; set; }

        [Required(ErrorMessage = "Grant Name is required")]
        public string GrantName { get; set; }  // Changed from string? to string

        public int? ProjectID { get; set; }

        [Required(ErrorMessage = "Supplier is required")]
        public string Supplier { get; set; }

        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Project is required")]
        public string Project { get; set; }  // Changed from string? to string

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public float Amount { get; set; }  

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Submission Date is required")]
        public DateTime SubmissionDate { get; set; }

        [Required(ErrorMessage = "Award Date is required")]
        public DateTime AwardDate { get; set; }
    }
}
