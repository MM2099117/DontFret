using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace DFShop.Models
{
    public class DontFretEntities : IdentityDbContext<ApplicationUser>
    {
           public DontFretEntities()
                : base("DontFretDB", throwIfV1Schema: false)
            {
            }

            public static DontFretEntities Create()
            {
                return new DontFretEntities();
            }
        
        /// <summary>
        /// DBSets for the models the shop is comprised of
        /// </summary>
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Supplier> Suppliers { get; set; }



    }
}