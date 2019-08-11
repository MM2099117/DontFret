using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFShop.Models
{
    public class RemoveCartViewModel
    {
        ///properties for deleting cart items
        public string Message { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public int ShoppingCartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }

    }
}