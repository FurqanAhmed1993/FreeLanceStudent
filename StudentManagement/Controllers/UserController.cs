using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using StudentManagement.Models;
using StudentManagement.Repository;


namespace StudentManagement.Controllers
{
    public class UserController : Controller
    {
        int LoginUserId = 1;
        // GET: User
        public ActionResult UserLoginList()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == "0")
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                LoginUserId = Convert.ToInt32(Session["UserId"].ToString());
            }
            List<UserLogin> lstUserLogin = new List<UserLogin>();
            DataTable dtUserLogin = new UserLoginRepository().Crud_UserLogin();
            lstUserLogin = Common.ConvertDataTable<UserLogin>(dtUserLogin);
            return View(lstUserLogin);
        }


        public ActionResult AddUserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUserLogin(UserLogin obj)
        {
            if (!string.IsNullOrEmpty(obj.UserName) && !string.IsNullOrEmpty(obj.LoginId) && !string.IsNullOrEmpty( obj.LoginPassword) && !string.IsNullOrEmpty( obj.EmailAddress))
            {
                UserLogin objUserLogin = new UserLogin();

                DataTable dtUserLogin = new UserLoginRepository().Crud_UserLogin(Common.Insert, 0, obj.UserName, obj.LoginId, obj.LoginPassword, obj.EmailAddress, true, LoginUserId);

                return RedirectToAction("UserLoginList", "User");
            }
            else
            {
                ViewBag.Message = "Please Fill all the fields";
                return View();
            }
        }
        public ActionResult EditUserLoginDetails(int Userid)
        {
            UserLogin objUserLogin = new UserLogin();


            DataTable dtUserLogin = new UserLoginRepository().Crud_UserLogin(Common.Select, Userid);
            if (dtUserLogin != null && dtUserLogin.Rows.Count > 0)
            {
                objUserLogin = Common.ConvertDataTable<UserLogin>(dtUserLogin)[0];

            }
            return View(objUserLogin);
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditUserLoginDetails(int UserId, UserLogin obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.UserName) && !string.IsNullOrEmpty(obj.LoginId) && !string.IsNullOrEmpty(obj.LoginPassword) && !string.IsNullOrEmpty(obj.EmailAddress))
                {
                    DataTable dt = new UserLoginRepository().Crud_UserLogin(Common.Update, UserId, obj.UserName, obj.LoginId, obj.LoginPassword, obj.EmailAddress, true, LoginUserId);
                    return RedirectToAction("UserLoginList", "User");
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
        public ActionResult DeleteUser(int UserId)
        {
            try
            {
                DataTable dt = new UserLoginRepository().Crud_UserLogin(Common.Delete, UserId);
                return RedirectToAction("UserLoginList", "User");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult LoginUser()
        {
            try
            {
                Session["UserId"] = "0";
                return View();

            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult LoginUser(UserLogin obj)
             
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataTable dtUserLogin = new UserLoginRepository().Crud_UserLogin(Common.Login, 0, null, obj.LoginId, obj.LoginPassword);
                    if (dtUserLogin.Rows.Count > 0)
                    {
                        Session["UserId"] = dtUserLogin.Rows[0]["UserId"].ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Your LoginId or Password is InCorrect";
                        //return RedirectToAction("LoginUser", "User");
                    }
                }
                else
                {
                    return RedirectToAction("LoginUser", "User");
                }
                

            }
            catch
            {
                return View();
            }
            return View();
        }
    }
}