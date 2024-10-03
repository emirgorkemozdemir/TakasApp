using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakasApp.Models;

namespace TakasApp.Controllers
{
    public class ProductController : Controller
    {

        // GET: Product
        TakasDBEntities2 db = new TakasDBEntities2();

        [HttpGet]
        public ActionResult MainPage()
        {
            int user_id = Convert.ToInt32(Session["LoggedUserID"]);
            int il_plakası = 33;
            var products = db.TableProduct
                      .Where(p=>p.ProductCity==il_plakası && p.ProductUser !=user_id)
                      .Take(30)
                      .ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(); // Ürün ekleme formu görünümünü döndür
        }

        // Ürün Ekleme İşlemi (POST: Product/Add)
        [HttpPost]
        public ActionResult Add(TableProduct product)
        {
            // Eğer form doğrulaması başarılıysa
            if (ModelState.IsValid)
            {
                product.ProductAddDate = DateTime.Now; // Ürünün eklenme tarihini ata
                db.TableProduct.Add(product); // Veritabanına ürünü ekle
                db.SaveChanges(); // Değişiklikleri kaydet
                TempData["Message"] = "Ürün başarıyla eklendi."; // Kullanıcıya bilgilendirme mesajı
                return RedirectToAction("MainPage", "Product"); // Ana sayfaya yönlendirme
            }

            // Form geçerli değilse formu tekrar göster
            return View(product);
        }

        public ActionResult GetProduct(int productId)
        {
            // Veritabanından ürün bilgisi getirilir
            var product = db.TableProduct.Find(productId);

            // Ürün bulunamadıysa, bir hata sayfası veya "Not Found" döndürülebilir
            if (product == null)
            {
                return HttpNotFound();
            }

            // Ürünü View ile döndür
            return View(product);
        }
    }
}