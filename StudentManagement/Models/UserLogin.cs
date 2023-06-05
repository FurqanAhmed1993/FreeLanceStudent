using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class UserLogin
    {
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter LoginId"), MaxLength(30)]
        public string LoginId { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Password"), MaxLength(30)]
        public string LoginPassword { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        
    }
}