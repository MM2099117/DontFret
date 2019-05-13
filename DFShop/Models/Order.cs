using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DFShop.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name required")]
        [DisplayName("Full Name")]
        [StringLength(160)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Address required")]
        [StringLength(70)]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Address required")]
        [StringLength(70)]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Town required")]
        [StringLength(40)]
        public string Town { get; set; }

        [Required(ErrorMessage = "Postcode required")]
        [DisplayName("Postcode")]
        [StringLength(10)]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Mobile Number required")]
        [StringLength(24)]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}