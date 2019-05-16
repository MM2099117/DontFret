using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DFShop.Models
{
    public class ShoppingCart
    {
        /// <summary>
        /// Shopping Cart Properties
        /// </summary>
       [Key]
        public int EntryID { get; set; }
        public string ShoppingCartID { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product{ get; set; }
        public int ProductQuantity { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

    }
}