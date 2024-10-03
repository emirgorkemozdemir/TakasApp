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
        public ActionResult ProductsToOffer(int productID, int productUser)
        {
            int user_id = Convert.ToInt32(Session["LoggedUserID"]);
            List<TableProduct> products = db.TableProduct.Where(p => p.ProductUser ==user_id).ToList();
            CustomOfferModel mymodel = new CustomOfferModel();
            mymodel.ProductID = productID;
            mymodel.ProductUser = productUser;
            mymodel.ProductList = products;
            return View(mymodel);
        }

        [HttpGet]
        public ActionResult MakeOffer(int givenid, int user,int takenid)
        {
            TableOffer offer = new TableOffer();
            offer.OfferRecieverID = user;
            offer.OfferProductID1 = givenid.ToString();
            offer.OfferProductID2 = takenid.ToString();
            offer.OfferSenderID =Convert.ToInt32(Session["LoggedUserID"]);

            db.TableOffer.Add(offer);
            db.SaveChanges();
            return RedirectToAction("MainPage", "Product");
        }
    }
}