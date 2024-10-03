using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakasApp.Models
{
    public class CustomOfferModel
    {
        public List<TableProduct> ProductList { get; set; }

        // Hangi ürüne teklif götürüyorsam onun idsi
        public int ProductID { get; set; }


        // Teklif götürdügüm ürünün sahibi
        public int ProductUser { get; set; }
    }
}