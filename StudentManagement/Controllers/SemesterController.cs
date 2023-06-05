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
    public class SemesterController : Controller
    {
        // GET: Term
        int UserId = 1;
        // GET: Semester
        public ActionResult SemesterList()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == "0")
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserId"].ToString());
            }
            List<Semester> lstSemester = new List<Semester>();
            DataTable dtSemester = new StudentRepository().Crud_Semester();
            lstSemester = Common.ConvertDataTable<Semester>(dtSemester);
            return View(lstSemester);
        }


        public ActionResult AddSemester()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSemester(Semester obj)
        {
            if (!string.IsNullOrEmpty(obj.SemesterName) && obj.SemesterYear > 0)
            {
                Semester objSemester = new Semester();

                DataTable dtSemester = new StudentRepository().Crud_Semester(Common.Insert, 0, obj.SemesterName, obj.SemesterYear, true, UserId);

                return RedirectToAction("SemesterList", "Semester");
            }
            else
            {
                ViewBag.Message = "Please fill all the fields";
                return View();
            }
        }
        public ActionResult EditSemesterDetails(int SemesterId)
        {
            Semester objSemester = new Semester();


            DataTable dtSemester = new StudentRepository().Crud_Semester(Common.Select, SemesterId);
            if (dtSemester != null && dtSemester.Rows.Count > 0)
            {
                objSemester = Common.ConvertDataTable<Semester>(dtSemester)[0];

            }
            return View(objSemester);
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditSemesterDetails(int SemesterId, Semester obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.SemesterName) && obj.SemesterYear > 0)
                {
                    DataTable dt = new StudentRepository().Crud_Semester(Common.Update, SemesterId, obj.SemesterName, obj.SemesterYear, true, UserId);
                    return RedirectToAction("SemesterList", "Semester");
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
        public ActionResult DeleteSemester(int SemesterId)
        {
            try
            {
                DataTable dt = new StudentRepository().Crud_Semester(Common.Delete, SemesterId);
                return RedirectToAction("SemesterList", "Semester");

            }
            catch
            {
                return View();
            }
        }
    }
}