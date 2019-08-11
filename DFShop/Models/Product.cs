using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

namespace DFShop.Models
{
    [Bind(Exclude = "ProductID")]
    public class Product
    {
        /// <summary>
        /// Product properties
        /// </summary>
        [ScaffoldColumn(false)]
        [Key]
        public int ProductID { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please enter a product name")]
        public string ProductName { get; set; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        [DisplayName("Supplier")]
        public int SupplierID { get; set; }

        public virtual Supplier Supplier { get; set; }

        [Required(ErrorMessage = "Please enter a product price")]
        public decimal Price { get; set; }

        [DisplayName("Upload Image")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [DisplayName("Quantity")]
        public int StockLevel { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public string ErrorMessage { get; set; }


    }
}