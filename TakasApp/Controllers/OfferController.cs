using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakasApp.Models;

namespace TakasApp.Controllers
{
    public class OfferController : Controller
    {
        TakasDBEntities2 db = new TakasDBEntities2();
        [HttpGet]
        public ActionResult ProductsToOffer()
        {
            int user_id = Convert.ToInt32(Session["LoggedUserID"]);
            List<TableProduct> products = db.TableProduct.Where(p => p.ProductUser ==user_id).ToList();
            return View(products);
        }
    }
}