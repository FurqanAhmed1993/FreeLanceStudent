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
    public class StudentController : Controller
    {
        int UserId = 1;
        // GET: Student
        public ActionResult StudentList()
        {
          
            if (Session["UserId"] == null || Session["UserId"].ToString() == "0")
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserId"].ToString());
            }
            List<Student> lstStudent = new List<Student>();
            DataTable dtStudent = new StudentRepository().Crud_Student();
            lstStudent= Common.ConvertDataTable<Student>(dtStudent);
            return View(lstStudent);
        }

        [HttpPost]
        public ActionResult StudentList(string FirstName,string LastName)
        {
            
            List<Student> lstStudent = new List<Student>();
            DataTable dtStudent = new StudentRepository().Crud_Student(Common.Select,0, FirstName,LastName);
            lstStudent = Common.ConvertDataTable<Student>(dtStudent);
            return View(lstStudent);
        }
        public ActionResult AddStudent()
        {
            Student objStudent = new Student();
          
            DataTable dtTerms = new StudentRepository().Crud_Terms();
            List<Terms> lstTerms = Common.ConvertDataTable<Terms>(dtTerms);
            objStudent.Terms = lstTerms;

            DataTable dtSemester = new StudentRepository().Crud_Semester();
            List<Semester> lstSemester = Common.ConvertDataTable<Semester>(dtSemester);
            objStudent.Semester = lstSemester;
            return View(objStudent);
        }

        [HttpPost]
        public ActionResult AddStudent(Student obj)
        {
            if (!string.IsNullOrEmpty(obj.FirstName) && !string.IsNullOrEmpty(obj.LastName) && !string.IsNullOrEmpty(obj.ParentName) && obj.TermId > 0 && obj.SemesterId > 0 && !string.IsNullOrEmpty(obj.EmailAddress))
            {
                Student objStudent = new Student();

                DataTable dtStudent = new StudentRepository().Crud_Student(Common.Insert, 0, obj.FirstName, obj.LastName, obj.ParentName, obj.TermId, obj.SemesterId,obj.EmailAddress, true, UserId);

                return RedirectToAction("StudentList", "Student");
            }
            else
            {
                ViewBag.Message = "Please Fill all the fields";
                return View();
            }
        }
        public ActionResult EditStudentDetails(int StudentId)
        {
            Student objStudent = new Student();
            
            DataTable dtTerms = new StudentRepository().Crud_Terms();
            List<Terms> lstTerms = Common.ConvertDataTable<Terms>(dtTerms);
           

            DataTable dtSemester = new StudentRepository().Crud_Semester();
            List<Semester> lstSemester = Common.ConvertDataTable<Semester>(dtSemester);
           
            DataTable dtStudent = new StudentRepository().Crud_Student(Common.Select, StudentId);
            if (dtStudent != null && dtStudent.Rows.Count > 0)
            {
                objStudent = Common.ConvertDataTable<Student>(dtStudent)[0];
                objStudent.Terms = lstTerms;
                objStudent.Semester = lstSemester;
            }
            return View(objStudent);
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditStudentDetails(int StudentId, Student obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.FirstName) && !string.IsNullOrEmpty(obj.LastName) && !string.IsNullOrEmpty(obj.ParentName) && obj.TermId > 0 && obj.SemesterId > 0 && !string.IsNullOrEmpty(obj.EmailAddress))
                {
                    DataTable dt = new StudentRepository().Crud_Student(Common.Update, StudentId,obj.FirstName,obj.LastName,obj.ParentName,obj.TermId,obj.SemesterId, obj.EmailAddress, true,UserId);
                return RedirectToAction("StudentList", "Student");
                }
                else
                {
                    ViewBag.Message = "Please Fill all the fields";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeleteStudent(int StudentId)
        {
            try
            {
                DataTable dt = new StudentRepository().Crud_Student(Common.Delete, StudentId);
                return RedirectToAction("StudentList", "Student");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult SendEmail(int StudentId)
        {
            try
            {
                DataTable dt = new StudentRepository().GetEmailData( StudentId);
                if(dt.Rows.Count>0)
                {
                    string Message=Common.GetMessage(Common.StudentPaymentEmailTemplate, dt);
                    if(!string.IsNullOrEmpty(Message))
                    {
                        Email.SendMails(dt.Rows[0]["EmailAddress"].ToString(), "Student", Message, "", "", "");
                    }

                }
                return RedirectToAction("StudentList", "Student");

            }
            catch
            {
                return View();
            }
        }
    }
}