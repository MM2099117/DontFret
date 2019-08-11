using DFShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DFShop.Controllers
{
    public class HomeController : Controller
    {
        DontFretEntities db = new DontFretEntities();

        private List<Product> GetBestSellers(int count)
        {
            return db.Products.OrderByDescending(i => i.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        public ActionResult Index()
        {
            var bestSellers = GetBestSellers(5);
            return View(bestSellers);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}