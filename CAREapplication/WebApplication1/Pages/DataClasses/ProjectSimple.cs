﻿using System.ComponentModel.DataAnnotations;

namespace CAREapplication.Pages.DataClasses
{
    public class ProjectSimple
    {
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "ProjectID is required")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "ProjectName is required")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "DueDate is required")]
        public float Amount { get; set; }
        public string ProjectDescription { get; set; }
    }
}
