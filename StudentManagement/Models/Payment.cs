using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Payment
    {
        public int PaymentDetailId { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
        [Display(Name = "Student Full Name")]
        public string StudentFullName { get; set; }
        public int StudentId { get; set; }
        
        public decimal Amount { get; set; }
        [Display(Name = "Unit Amount Inc Tax")]
        public decimal UnitAmountIncTax { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        public int BankId { get; set; }
        [Display(Name = "Transaction Type Code")]
        public string TransactionTypeCode { get; set; }
        public int TransactionTypeId { get; set; }
        [Display(Name = "Payment Final Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string PaymentFinalDate { get; set; }


        [Display(Name = "Students")]
        public IEnumerable<Student> Students { get; set; }

        [Required]
        [Display(Name = "Banks")]
        public IEnumerable<Bank> Banks { get; set; }

        [Required]
        [Display(Name = "TransactionTypes")]
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
    }
}

