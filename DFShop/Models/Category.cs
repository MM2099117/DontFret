using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFShop.Models
{
    public class Category
    {
        /// <summary>
        /// Category properties
        /// </summary>
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }

        /// <summary>
        /// navigational properties for category 
        /// </summary>
        public virtual List<Product> Products { get; set; }
    }
}