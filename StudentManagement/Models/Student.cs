using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
       
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Student Full Name")]
        public string StudentFullName { get; set; }
       [Required]
        [Display(Name = "Parent Name")]
        public string ParentName { get; set; }

        [Display(Name = "Term Name")]
        public string TermName { get; set; }
        [Display(Name = "Semester Full Name")]
        public string SemesterFullName { get; set; }
        [Display(Name = "Term Name")]
        public int? TermId { get; set; }
        [Display(Name = "Semester Name")]
        public int? SemesterId { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Terms")]
        public IEnumerable<Terms> Terms { get; set; }

        [Required]
        [Display(Name = "Semester")]
        public IEnumerable<Semester> Semester { get; set; }
    }
    
   
}