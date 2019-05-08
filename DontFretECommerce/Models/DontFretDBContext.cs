using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DontFretECommerce.Models
{
    public class DontFretDBContext : DbContext
    {

        /// <summary>
        /// DBSets for entity framework collections
        /// </summary>
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }




    }
}