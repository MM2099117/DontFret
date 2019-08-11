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
            newEntry.StockLevel--;

            var cart = Cart.GetCart(this.HttpContext);

            cart.AddToCart(newEntry);

            return RedirectToAction("Index");

        }

        /// <summary>
        /// POST: Remove cart entry
        /// </summary>
        /// <param name="entryID"></param>
        /// <returns></returns>
        public ActionResult RemoveEntry(int id)
        {
            var cart = Cart.GetCart(this.HttpContext);

            string productName = db.ShoppingCarts.Single(s => s.EntryID == id).Product.ProductName;

            int entryCount = cart.RemoveEntry(id);

            var results = new RemoveCartViewModel
            {
                Message = Server.HtmlEncode(productName) +
                              " has been removed from your shopping cart.",
                ShoppingCartTotal = cart.GetCartTotal(),
                ShoppingCartCount = cart.GetCartCount(),
                ItemCount = entryCount,
                DeleteId = id
            };
            return Json(results);

        }

        /// <summary>
        /// Shows the user how many times are currently in their cart in the navbar
        /// </summary>
        /// <returns>partial cart view</returns>
        [ChildActionOnly]
        public ActionResult GetCartSummary()
        {
            var shoppingCart = Cart.GetCart(this.HttpContext);

            ViewData["CartEntryCount"] = shoppingCart.GetCartCount();
            return PartialView("CartSummary");
        }
    }
}