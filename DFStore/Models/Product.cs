using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFStore.Models
{
    public class Product
    {
        /// <summary>
        /// Product properties
        /// </summary>
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public Supplier Supplier { get; set; }
    }
}