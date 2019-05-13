using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFShop.Models
{
    public class RemoveCartViewModel
    {
        ///properties for deleting cart items
        public string ConfirmationMessage { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public int CartEntriesCount { get; set; }
        public int ProductCount { get; set; }
        public int DeleteID { get; set; }

    }
}