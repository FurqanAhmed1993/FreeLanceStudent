using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentManagement.Models;
using StudentManagement.Repository;

namespace StudentManagement.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        int UserId = 1;
        // GET: Payment
        public ActionResult PaymentList()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == "0")
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserId"].ToString());
            }
            List<Payment> lstPayments = new List<Payment>();
            DataTable dtPayment = new PaymentRepository().Crud_PaymentDetail();
            lstPayments = Common.ConvertDataTable<Payment>(dtPayment);
            return View(lstPayments);
        }

        
        public ActionResult AddPayment()
        {
            Payment objPayment = new Payment();

            DataTable dtStudent = new StudentRepository().Crud_Student();
            List<Student> lstStudent = Common.ConvertDataTable<Student>(dtStudent);
            objPayment.Students = lstStudent;

            DataTable dtBanks = new BankRepository().Crud_BankMaster();
            List<Bank> lstBanks = Common.ConvertDataTable<Bank>(dtBanks);
            objPayment.Banks = lstBanks;

            DataTable dtTransactionTypes = new PaymentRepository().Crud_TransactionType();
            List<TransactionType> lstTransactionTypes = Common.ConvertDataTable<TransactionType>(dtTransactionTypes);
            objPayment.TransactionTypes = lstTransactionTypes;

            return View(objPayment);
        }

        [HttpPost]
        public ActionResult AddPayment(Payment obj)
        {
            if (!string.IsNullOrEmpty(obj.RefNo) && !string.IsNullOrEmpty(obj.PaymentFinalDate)
                && obj.Amount>0 && obj.UnitAmountIncTax>0 && obj.StudentId>0 && obj.TransactionTypeId>0 )
            {
                Payment objStudent = new Payment();

                DataTable dtStudent = new PaymentRepository().Crud_PaymentDetail(Common.Insert, 0, obj.RefNo, obj.StudentId, obj.Amount, obj.UnitAmountIncTax, obj.BankId, obj.TransactionTypeId, obj.PaymentFinalDate, true, UserId);

                return RedirectToAction("PaymentList", "Payment");
            }
            else
            {
                ViewBag.Message = "Please fill all the fields and amount must be greater than 0 ";
                return View();
            }
        }
        public ActionResult EditPaymentDetails(int PaymentDetailId)
        {
            Payment objPayment = new Payment();

            DataTable dtStudent = new StudentRepository().Crud_Student();
            List<Student> lstStudent = Common.ConvertDataTable<Student>(dtStudent);
            

            DataTable dtBanks = new BankRepository().Crud_BankMaster();
            List<Bank> lstBanks = Common.ConvertDataTable<Bank>(dtBanks);
          

            DataTable dtTransactionTypes = new PaymentRepository().Crud_TransactionType();
            List<TransactionType> lstTransactionTypes = Common.ConvertDataTable<TransactionType>(dtTransactionTypes);
           

            DataTable dtPayment = new PaymentRepository().Crud_PaymentDetail(Common.Select, PaymentDetailId);
            if (dtPayment != null && dtPayment.Rows.Count > 0)
            {
                objPayment = Common.ConvertDataTable<Payment>(dtPayment)[0];
                objPayment.Students = lstStudent;
                objPayment.Banks = lstBanks;
                objPayment.TransactionTypes = lstTransactionTypes;
                objPayment.PaymentFinalDate = Convert.ToDateTime(objPayment.PaymentFinalDate).ToString("MM/dd/yyyy");
            }
            return View(objPayment);
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditPaymentDetails(int PaymentDetailId, Payment obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.RefNo) && !string.IsNullOrEmpty(obj.PaymentFinalDate)
                    && obj.Amount > 0 && obj.UnitAmountIncTax > 0 && obj.StudentId > 0 && obj.TransactionTypeId > 0)
                {
                    DataTable dt = new PaymentRepository().Crud_PaymentDetail(Common.Update, PaymentDetailId, 
                    obj.RefNo, obj.StudentId, obj.Amount, obj.UnitAmountIncTax, obj.BankId,obj.TransactionTypeId, obj.PaymentFinalDate, true, UserId);
                return RedirectToAction("PaymentList", "Payment");
                }
                else
                {
                    ViewBag.Message = "Please fill all the fields and amount must be greater than 0 ";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeletePaymentDetails(int PaymentDetailId)
        {
            try
            {
                DataTable dt = new PaymentRepository().Crud_PaymentDetail(Common.Delete, PaymentDetailId);
                return RedirectToAction("PaymentList", "Payment");

            }
            catch
            {
                return View();
            }
        }
    }
}