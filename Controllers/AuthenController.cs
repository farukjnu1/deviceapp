using DeviceExamine.BLL;
using DeviceExamine.Models;
using DeviceExamine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DeviceExamine.Controllers
{
    public class AuthenController : Controller
    {
        // Developed by
        // Faruk Abdullah
        // Cell: +8801920334664
        // Email: farukjnu1@gmail.com

        // Date: 15-Apr-2016

        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(UserVm u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBLL empB = new EmployeeBLL();
                //New Code Start
                UserStatus status = empB.GetUserValidity(u);
                string IsAdmin = "0";
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = "1";
                }
                else if (status == UserStatus.AuthentucatedUser)
                {
                    IsAdmin = "2";
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Index");
                }

                // Login info
                LoginInfo li = new LoginInfo();
                li.Username = u.UserName;
                li.LoginTime = DateTime.Now;
                int nEffected = empB.AddLoginInfo(li);
                if (nEffected > 0)
                {
                    FormsAuthentication.SetAuthCookie(u.UserName, false);
                    Session["IsAdmin"] = IsAdmin;
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Error in log info");
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}