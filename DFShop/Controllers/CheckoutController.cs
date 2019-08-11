using DFShop.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DFShop.Controllers
{
    /// <summary>
    /// Controller to manage orders, payment and billing addresses
    /// </summary>
    [Authorize]
    public class CheckoutController : Controller
    {
        /// <summary>
        /// new instance of DB
        /// </summary>
        DontFretEntities db = new DontFretEntities();

        /// <summary>
        /// string to hold promotional offer code
        /// </summary>
        const string OfferCode = "DontFret25";
     
        
        /// <summary>
        /// GET: Open form for user to enter address and payment info
        /// </summary>
        /// <returns>form</returns>
        public ActionResult PaymentForm()
        {
            return View();
        }

        /// <summary>
        /// POST: Gets address and payment data to make an order
        /// </summary>
        /// <param name="data"></param>
        /// <returns>order summary</returns>
        [HttpPost]
        public ActionResult PaymentForm(FormCollection data, string stripeToken)
        {

            var order = new Models.Order();
            ///updates the order model with the new data
            TryUpdateModel(order);
            try
            {
                ///if the promo code entered by the user matches the stored promo code
                if (string.Equals(data["OfferCode"], OfferCode, StringComparison.OrdinalIgnoreCase) != false)
                   
                {
                    ///creates a new order associated with the current user at the current time
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    decimal cartTotal = Cart.GetCart(this.HttpContext).GetCartTotal();
                    decimal promoTotal = cartTotal / 100 * 25;
                    cartTotal = cartTotal - promoTotal;
                    order.Total = cartTotal;

                    ///adds the order to the database
                    db.Orders.Add(order);
                    db.SaveChanges();

                    ///gets the cart and makes the new order
                    var shoppingCart = Cart.GetCart(this.HttpContext);
                    shoppingCart.MakeOrder(order);

                    ///redirects to the order complete page, passing in the new order ID
                    return RedirectToAction("OrderComplete", order);
                }
                ///if the promo code is empty
                else if (string.Equals(data["OfferCode"], "", StringComparison.OrdinalIgnoreCase) != false)
                {
                    ///creates a new order associated with the current user at the current time
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    decimal cartTotal = Cart.GetCart(this.HttpContext).GetCartTotal();
                    order.Total = cartTotal;

                    ///adds the order to the database
                    db.Orders.Add(order);
                    db.SaveChanges();

                    ///gets the cart and makes the new order
                    var shoppingCart = Cart.GetCart(this.HttpContext);
                    shoppingCart.MakeOrder(order);
                   
                    ///redirects to the order complete page, passing in the new order details
                    return RedirectToAction("OrderComplete", order);
                }
                else
                {
                    return View(order);
                }
            }
            catch
            {
                ///returns the order view as normal if something goes wrong
                return View(order);
            }
        }

        /// <summary>
        /// verifies the order and current user, then confirms the order
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>view with order details or error message</returns>
        public ActionResult OrderComplete(Models.Order order)
        { 
            bool isOrderValid = db.Orders.Any(o => o.OrderId == order.OrderId && o.Username == User.Identity.Name);

            if (isOrderValid)
            {   
                return View(order);
            }
            else
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Shows a summary of the placed order in a view
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns>view of the order</returns>
        public ActionResult OrderSummary(int id)
        {
            IEnumerable<OrderDetail> orderDetail = db.OrderDetails.Where(o => o.OrderID == id && o.Order.Username == User.Identity.Name).ToList();

            return View(orderDetail);
        }
    }
}