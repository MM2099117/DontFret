using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DFShop.Models
{
    public class CartViewModel
    {
        /// <summary>
        /// properties for the view model
        /// </summary>
        public List<ShoppingCart> CartEntries { get; set; }

        [Key]
        public int CartID { get; set; }

        public decimal ShoppingCartTotal { get; set; }



    }
}