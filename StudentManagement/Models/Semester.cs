using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Semester
    {
        public int SemesterId { get; set; }
        [Display(Name = "Semester Name")]
        public string SemesterName { get; set; }
        [Display(Name = "Semester Full Name")]
        public string SemesterFullName { get; set; }
        [Display(Name = "Semester Year")]
        public int SemesterYear { get; set; }
    }
}