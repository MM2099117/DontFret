using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DFShop.Models;
using System.Web.Mvc;

namespace DFShop.Controllers
{
    public class CartController : Controller
    {
        /// global DB instance
        DontFretEntities db = new DontFretEntities();

        /// <summary>
        /// GET: Cart
        /// </summary>
        /// <returns>cart view</returns>
        public ActionResult Index()
        {
            var shoppingCart = Cart.GetCart(this.HttpContext);

            var model = new CartViewModel
            {
                CartEntries = shoppingCart.GetCartEntries(),
                ShoppingCartTotal = shoppingCart.GetCartTotal()
            };

            return View(model);
        }

        /// <summary>
        /// Adds the current product to the cart
        /// </summary>
        /// <param name="productID"></param>
        /// <returns>Cart view with product added</returns>
        public ActionResult AddToCart(int productID)
        {
            var newEntry = db.Products.Single(p => p.ProductID == productID);

            var cart = Cart.GetCart(this.HttpContext);

            cart.AddToCart(newEntry);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult RemoveEntry(int entryID)
        {
            var cart = Cart.GetCart(this.HttpContext);

            string productName = db.ShoppingCarts.Single(s => s.EntryID == entryID).Product.ProductName;

            int entryCount = cart.RemoveEntry(entryID);

            var results = new RemoveCartViewModel
            {
                ConfirmationMessage = Server.HtmlEncode(productName) + " was successfully removed.",
                ShoppingCartTotal = cart.GetCartTotal(),
                CartEntriesCount = cart.GetCartCount(),
                ProductCount = entryCount,
                DeleteID = entryID

            };
            return Json(results);

        }

        [ChildActionOnly]
        public ActionResult GetCartSummary()
        {
            var shoppingCart = Cart.GetCart(this.HttpContext);

            ViewData["CartEntryCount"] = shoppingCart.GetCartCount();
            return PartialView("CartSummary");
        }
    }
}