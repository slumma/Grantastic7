using System;
using System.ComponentModel.DataAnnotations;

namespace lab484.Pages.Data_Classes
{
    public class GrantSimple
    {
        public int GrantID { get; set; }

        [Required(ErrorMessage = "Grant Name is required")]
        public String? GrantName { get; set; }
        public int? ProjectID { get; set; }

        [Required(ErrorMessage = "Supplier is required")]
        public string Supplier { get; set; }

        public int SupplierID { get; set; }
        [Required(ErrorMessage = "Project is required")]
        public String? Project { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
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
