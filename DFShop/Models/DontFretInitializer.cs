using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFShop.Models
{
    public class DontFretInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DontFretEntities>
    {
        protected override void Seed(DontFretEntities context)
        {
            if (!context.Users.Any())
            {
                var Admin = new ApplicationUser()
                {
                    FullName = "Admin",
                    Add1 = "Dont Fret Music",
                    Add2 = "111 High Street",
                    Town = "Townsville",
                    Postcode = "G3 8PR",
                    ContactNumber = "0141 435 8999"

                };

            }

            var categories = new List<Category>
            {
                new Category { CategoryName = "Electric" , CategoryDescription = "Axes of all your favourite brands, from the classic Fender to the Yamahas"},
                new Category { CategoryName = "Acoustic" , CategoryDescription = "Acoustic and Semi Acoustic Guitars; for when you just need to blast Wonderwall at a stale party"},
                new Category { CategoryName = "Amplifiers", CategoryDescription = "From Deacy to Line 6, we've got the tools to bring the noise." },
                new Category { CategoryName = "Accessories", CategoryDescription = "The littlest things can make a big difference in music" },
            };
            context.SaveChanges();


            var Suppliers = new List<Supplier>
            {
                new Supplier { SupplierName = "Martin" },
                new Supplier { SupplierName = "Taylor" },
                new Supplier { SupplierName = "Gibson" },
                new Supplier { SupplierName = "Guild" },
                new Supplier { SupplierName = "Seagull" },
                new Supplier { SupplierName = "Yamaha" },
                new Supplier { SupplierName = "Fender" },
                new Supplier {SupplierName = "Line 6" },
                new Supplier {SupplierName = "Deacy"},
                new Supplier {SupplierName = "Blackstar"},
                new Supplier {SupplierName = "VTL"}



            };
            context.SaveChanges();


            new List<Product>
            {
                new Product { ProductName = "Gibson Electric 1998", Category = categories.Single(g => g.CategoryName == "Electric"), Price = 1299.99M, Supplier = Suppliers.Single(a => a.SupplierName == "Gibson"), ImagePath = "/Content/img/image1.jpg", StockLevel = 10 },
                new Product { ProductName = "Martin Acoustic 2018", Category = categories.Single(g => g.CategoryName == "Acoustic"), Price = 346.99M, Supplier = Suppliers.Single(a => a.SupplierName == "Martin"), ImagePath = "/Content/img/image2.jpg", StockLevel =  8 },
                new Product { ProductName = "Fender Carbon Nylon Plectrums", Category = categories.Single(g=> g.CategoryName == "Accessories"), Price = 8.99M, Supplier = Suppliers.Single(a=>a.SupplierName == "Fender"), ImagePath = "/Content/img/image5.jpg", StockLevel = 10  },
                new Product { ProductName = "VTL Amp Classic", Category = categories.Single(g=> g.CategoryName == "Amplifiers"), Price = 400.00M, Supplier = Suppliers.Single(a=>a.SupplierName == "VTL"), ImagePath = "/Content/img/image4.jpg", StockLevel = 250 }
            }.ForEach(a => context.Products.Add(a));
            context.SaveChanges();



        }

    }
}