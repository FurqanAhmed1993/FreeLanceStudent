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
    public class BankController : Controller
    {

        int UserId = 1;
        // GET: Bank
        public ActionResult BankList()
        {
            
            if (Session["UserId"] == null || Session["UserId"].ToString() == "0")
            {
                
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserId"].ToString());
            }
            List<Bank> lstBank = new List<Bank>();
            DataTable dtBank = new BankRepository().Crud_BankMaster();
            lstBank = Common.ConvertDataTable<Bank>(dtBank);
            return View(lstBank);
        }


        public ActionResult AddBank()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult AddBank(Bank obj)
        {
            if (!string.IsNullOrEmpty(obj.BankName) && !string.IsNullOrEmpty(obj.AccountName)
                && !string.IsNullOrEmpty(obj.AccountNumber) && !string.IsNullOrEmpty(obj.BSBNumber))
            {
                Bank objBank = new Bank();

                DataTable dtBank = new BankRepository().Crud_BankMaster(Common.Insert, 0, obj.BankName, obj.AccountName, obj.BSBNumber, obj.AccountNumber, true, UserId);

                return RedirectToAction("BankList", "Bank");
            }
            else
            {
                ViewBag.Message = "Please fill all the fields";
                return View();
            }
            
        }
        public ActionResult EditBankDetails(int BankId)
        {
            Bank objBank = new Bank();

           
            DataTable dtBank = new BankRepository().Crud_BankMaster(Common.Select, BankId);
            if (dtBank != null && dtBank.Rows.Count > 0)
            {
                objBank = Common.ConvertDataTable<Bank>(dtBank)[0];
               
            }
            return View(objBank);
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditBankDetails(int BankId, Bank obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.BankName) && !string.IsNullOrEmpty(obj.AccountName)
                && !string.IsNullOrEmpty(obj.AccountNumber) && !string.IsNullOrEmpty(obj.BSBNumber))
                {
                    DataTable dt = new BankRepository().Crud_BankMaster(Common.Update, BankId, obj.BankName, obj.AccountName, obj.BSBNumber, obj.AccountNumber, true, UserId);
                return RedirectToAction("BankList", "Bank");
                }
                else
                {
                    ViewBag.Message = "Please fill all the fields";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeleteBank(int BankId)
        {
            try
            {
                DataTable dt = new BankRepository().Crud_BankMaster(Common.Delete, BankId);
                return RedirectToAction("BankList", "Bank");

            }
            catch
            {
                return View();
            }
        }
    }
}