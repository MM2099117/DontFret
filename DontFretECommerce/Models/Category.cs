using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DontFretECommerce.Models
{
    public partial class Category
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
        public List<Product> products { get; set; }


    }
}