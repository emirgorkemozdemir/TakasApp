using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
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
                registered_user.UserRegisterDate = DateTime.Now;
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
            var logged_user = db.TableUser.Where(u => u.UserName == selected_user.UserName && u.UserPassword == hashed_password).SingleOrDefault();

            if (logged_user == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                Session["LoggedUserID"] = logged_user.UserID;
                Session["IsUserLogged"] = true;
                return RedirectToAction("MainPage", "Product");
            }
        }


        [HttpGet]
        public ActionResult Myprofile()
        {
            if (Convert.ToBoolean(Session["LoggedUserID"]) == true)
            {
                int myid = Convert.ToInt32(Session["loggeduserID"]);
                var selected_user = db.TableUser.Find(myid);
                return View(selected_user);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult GetProfile(int user_id)
        {
            if (Convert.ToBoolean(Session["LoggedUserID"]) == true)
            {
                var selected_user = db.TableUser.Find(user_id);
                return View(selected_user);
            }
            else
            {
                return RedirectToAction("Login");
            }
         
        }

        [HttpGet]
        public ActionResult ListMyProducts()
        {

            if (Convert.ToBoolean(Session["LoggedUserID"]) == true)
            {
                int userId = Convert.ToInt32(Session["UserID"]);

                List<TableProduct> product_list = db.TableProduct.Where(p => p.ProductUser == userId).ToList();

                return View(product_list);
            }
            else
            {
                return RedirectToAction("Login");
            }
         
        }


        [HttpGet]
        public ActionResult ListUsersProducts(int userId)
        {
            if (Convert.ToBoolean(Session["LoggedUserID"]) == true)
            {
                List<TableProduct> product_list = db.TableProduct.Where(p => p.ProductUser == userId).ToList();

                return View(product_list);
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }


        [HttpGet]
        public ActionResult DeleteProduct(int pid)
        {
            var productForDelete = db.TableProduct.Find(pid);

            db.TableProduct.Remove(productForDelete);
            db.SaveChanges();

            return RedirectToAction("ListMyProducts");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }

}
