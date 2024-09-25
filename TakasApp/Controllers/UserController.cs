using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakasApp.App_Start;
using TakasApp.Models;

namespace TakasApp.Controllers
{
    public class UserController : Controller
    {
        TakasDBEntities2 db = new TakasDBEntities2();

        [HttpGet]
        public ActionResult UserAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserAdd(TableUser registered_user)
        {
            if (ModelState.IsValid)
            {
                registered_user.UserRegisterDate=DateTime.Now.Date;
                registered_user.UserPassword = Sha256Compute.ComputeSha256Hash(registered_user.UserPassword);
                db.TableUser.Add(registered_user);
                db.SaveChanges();
            }
            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TableUser selected_user)
        {
            string hashed_password = Sha256Compute.ComputeSha256Hash(selected_user.UserPassword);
            var logged_user= db.TableUser.Where(u => u.UserName == selected_user.UserName && u.UserPassword == hashed_password).SingleOrDefault();

            if (logged_user==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                Session["LoggedUserID"] = logged_user.UserID;
                return RedirectToAction("MainPage","Product");
            }   
          
        }

        
    }
}