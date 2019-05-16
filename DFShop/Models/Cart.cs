using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DFShop.Models
{

    /// <summary>
    /// This class handles the logic of the shopping cart, 
    /// interaction with controller, and non-registered customers being able to add to the cart
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Database Context Instance
        /// </summary>
        DontFretEntities db = new DontFretEntities();

        /// <summary>
        /// Cart ID Property
        /// </summary>
        string CartID { get; set; }

        /// <summary>
        /// Session Key for keeping cart active
        /// </summary>
        public const string SessionKey = "CartID";

        /// <summary>
        /// GET: Cart
        /// </summary>
        /// <param name="context"></param>
        /// <returns>cart object</returns>
        public static Cart GetCart(HttpContextBase context)
        {
            var cart = new Cart();
            cart.CartID = cart.GetCartID(context);
            return cart;
        }

        /// <summary>
        /// Help Method for controller
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static Cart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        /// <summary>
        /// Counts the number of entries in the cart
        /// </summary>
        /// <returns></returns>
        public int GetCartCount()
        {
            int? cartCount = (from cartEntries in db.ShoppingCarts where cartEntries.ShoppingCartID == CartID
                              select (int?)cartEntries.Count).Sum();

            ///returns the count or 0 if there are no entries
            return cartCount ?? 0;
        }

        /// <summary>
        /// Method for adding products to cart
        /// </summary>
        /// <param name="productID"></param>
        public void AddToCart(Product product)
        {
            var CartEntry = db.ShoppingCarts.SingleOrDefault(p => p.ShoppingCartID == CartID && p.ProductID == product.ProductID);

            ///if no valid entry
            if (CartEntry == null)
            {
                ///create new cart with the current product as an entry
                CartEntry = new ShoppingCart
                {
                    ProductID = product.ProductID,
                    ShoppingCartID = CartID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                ///update database
                db.ShoppingCarts.Add(CartEntry);
            }
            else
            {
                ///else add 1 to the count of cart entries
                CartEntry.Count++;
            }

            ///updates the database
            db.SaveChanges();
        }

        /// <summary>
        /// Method for removing items from the card
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        public int RemoveEntry(int EntryID)
        {
            var CartEntries = db.ShoppingCarts.Single(c => c.ShoppingCartID == CartID && c.EntryID == EntryID);

            int EntryCount = 0;

            ///if a product exists in this cart
            if (CartEntries != null)
            {
                ///and if more than 1 entry exists
                if (CartEntries.Count > 1)
                {
                    ///remove one from the entry count
                    CartEntries.Count--;
                    EntryCount = CartEntries.Count;
                }
                else
                {
                    ///remove that cart entry from DB
                    db.ShoppingCarts.Remove(CartEntries);
                }
                ///update database
                db.SaveChanges();
            }
            return EntryCount;

        }

        /// <summary>
        /// method to empty the cart
        /// </summary>
        public void ClearCart()
        {
            ///gets the current Cart
            var CartEntries = db.ShoppingCarts.Where(c => c.ShoppingCartID == CartID);
            

            ///iterates through the cart and removes all entries
            foreach (var entry in CartEntries)
            {
                db.ShoppingCarts.Remove(entry);
            }

            ///update database
            db.SaveChanges();
        }

        /// <summary>
        /// gets a list of the cart items
        /// </summary>
        /// <returns>list of cart entries</returns>
        public List<ShoppingCart> GetCartEntries()
        {
            return db.ShoppingCarts.Where(s => s.ShoppingCartID == CartID).ToList();
        }

        /// <summary>
        /// returns the total cost of the cart entries
        /// </summary>
        /// <returns>order + total</returns>
        public decimal GetCartTotal()
        {
            ///calculates the total from all the cart entries on the current cartID
            decimal? CartTotal = (from CartEntries in db.ShoppingCarts
                                  where CartEntries.ShoppingCartID == CartID
                                  select (int?)CartEntries.Count *
                                    CartEntries.Product.Price).Sum();

            ///returns cart total if there is a cart total, otherwise returns 0
            return CartTotal ?? decimal.Zero;


        }

        /// <summary>
        /// Method for making an order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int MakeOrder(Order order)
        {
            decimal OrderTotal = 0;

            ///gets the current cart entries by method call
            var CartEntries = GetCartEntries();

            ///iterates through the cart 
            foreach (var entry in CartEntries)
            {
                ///creates an orderdetail object comprised of each cart entry
                var OrderDetail = new OrderDetail
                {
                    ProductID = entry.ProductID,
                    OrderID = order.OrderId,
                    UnitPrice = entry.Product.Price,
                    Quantity = entry.Count
                };

                ///sets the order total to the total price of the products in the cart entry
                OrderTotal += (entry.Count * entry.Product.Price);

                ///adds the order detail to the database
                db.OrderDetails.Add(OrderDetail);
            }

            ///sets the order total variable to the new order total value
            order.Total = OrderTotal;

            ///updates the database
            db.SaveChanges();

            ///clears the card via method call
            ClearCart();

            ///returns the new order ID
            return order.OrderId;

        }

        /// <summary>
        /// gets the current cart ID from the session
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetCartID(HttpContextBase context)
        {
            ///checks if the current session cart is null
            if (context.Session[SessionKey] == null)
            {
                ///if the current user's name is not null and/or not empty
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    ///retrieve the session by key for that user
                    context.Session[SessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    ///create a new temporary global unique identifier
                    Guid tempID = Guid.NewGuid();

                    ///sets the current session ID to the new temp session ID
                    context.Session[SessionKey] = tempID.ToString();
                }
            }
            ///returns the session ID as a string
            return context.Session[SessionKey].ToString();
        }

        /// <summary>
        /// loops through the current user's cart and updates the DB
        /// </summary>
        /// <param name="userName"></param>
        public void UpdateUserCart(string Email)
        {
            var UserCart = db.ShoppingCarts.Where(s => s.ShoppingCartID == CartID);

            foreach (ShoppingCart s in UserCart)
            {
                s.ShoppingCartID = Email;
            }
            db.SaveChanges();




        }
    }

}
