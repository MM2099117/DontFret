using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DFStore.Models
{
    

    public class DontFretDBContext : IdentityDbContext<ApplicationUser>
    {
        public DontFretDBContext()
            : base("DontFretDB05", throwIfV1Schema: false)
        {
        }

        public static DontFretDBContext Create()
        {
            return new DontFretDBContext();
        }

        /// <summary>
        /// DBSets for entity framework collections
        /// </summary>
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}