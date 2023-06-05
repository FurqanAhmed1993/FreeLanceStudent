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
    public class TermsController : Controller
    {
        // GET: Term
        int UserId = 1;
        // GET: Terms
        public ActionResult TermsList()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == "0")
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserId"].ToString());
            }
            List<Terms> lstTerms = new List<Terms>();
            DataTable dtTerms = new StudentRepository().Crud_Terms();
            lstTerms = Common.ConvertDataTable<Terms>(dtTerms);
            return View(lstTerms);
        }


        public ActionResult AddTerms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTerms(Terms obj)
        {
            Terms objTerms = new Terms();

            DataTable dtTerms = new StudentRepository().Crud_Terms(Common.Insert, 0, obj.TermName, obj.TermStartDate, obj.TermEndDate,true, UserId);

            return RedirectToAction("TermsList", "Terms");
        }
        public ActionResult EditTermsDetails(int TermsId)
        {
            Terms objTerms = new Terms();


            DataTable dtTerms = new StudentRepository().Crud_Terms(Common.Select, TermsId);
            if (dtTerms != null && dtTerms.Rows.Count > 0)
            {
                objTerms = Common.ConvertDataTable<Terms>(dtTerms)[0];

            }
            return View(objTerms);
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditTermsDetails(int TermsId, Terms obj)
        {
            try
            {

                DataTable dt = new StudentRepository().Crud_Terms(Common.Update, TermsId, obj.TermName, obj.TermStartDate, obj.TermEndDate, true, UserId);
                return RedirectToAction("TermsList", "Terms");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeleteEmp(int TermsId)
        {
            try
            {
                DataTable dt = new StudentRepository().Crud_Terms(Common.Delete, TermsId);
                return RedirectToAction("TermsList", "Terms");

            }
            catch
            {
                return View();
            }
        }
    }
}