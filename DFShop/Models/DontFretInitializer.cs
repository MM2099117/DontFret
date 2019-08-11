using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

            ///creates an initial seed of the site roles
            if (!context.Roles.Any())
            {
                this.CreateRole(context, "Store Manager");
                this.CreateRole(context, "Assistant Manager");
                this.CreateRole(context, "Sales Assistant");
                this.CreateRole(context, "Customer");
                this.CreateRole(context, "Corporate Customer");
                this.CreateRole(context, "Invoice Clerk");

            }
            context.SaveChanges();
          
            if (!context.Users.Any())
            {
                this.CreateUser(context, "StoreManager@dontfret.co.uk", "Mark McNulty", "DontFret123!");
                this.SetRoleToUser(context, "StoreManager@dontfret.co.uk", "Store Manager");

                this.CreateUser(context, "AssistantManager@dontfret.co.uk", "James Lawn", "DontFret123!");
                this.SetRoleToUser(context, "AssistantManager@dontfret.co.uk", "Assistant Manager");

                this.CreateUser(context, "sales@dontfret.co.uk", "Aidan Rooney", "DontFret123!");
                this.SetRoleToUser(context, "sales@dontfret.co.uk", "Sales Assistant");

                this.CreateUser(context, "testemail@email.co.uk", "Daniel Carswell", "DontFret123!");
                this.SetRoleToUser(context, "testemail@email.co.uk", "Customer");

                this.CreateUser(context, "corporate@guitarguitar.co.uk", "Guitar Guitar", "DontFret123!");
                this.SetRoleToUser(context, "corporate@guitarguitar.co.uk", "Corporate Customer");

                this.CreateUser(context, "billing@dontfret.co.uk", "David Campbell", "DontFret123!");
                this.SetRoleToUser(context, "billing@dontfret.co.uk", "Invoice Clerk");

            }
            context.SaveChanges();


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
                new Product { ProductName = "VTL Amp Classic", Category = categories.Single(g=> g.CategoryName == "Amplifiers"), Price = 400.00M, Supplier = Suppliers.Single(a=>a.SupplierName == "VTL"), ImagePath = "/Content/img/image4.jpg", StockLevel = 250 },
            }.ForEach(a => context.Products.Add(a));
            context.SaveChanges();



        }

        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="context"></param>
        /// <param name="roleName"></param>
        private void CreateRole(DontFretEntities context, string roleName)
        {
            ///new instance of the RoleManager for the IdentityRoles in EntityFramework
            var roleMngr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ///sets the result to the newly created role
            var result = roleMngr.Create(new IdentityRole(roleName));

            ///if the result is unsuccessful
            if (!result.Succeeded)
            {
                ///displays the errors seperated by a semicolon
                throw new Exception(string.Join(";", result.Errors));
            }
        }

        /// <summary>
        /// Creates a new user at DB creation
        /// </summary>
        /// <param name="context"></param>
        /// <param name="email"></param>
        /// <param name="fullName"></param>
        /// <param name="password"></param>
        public void CreateUser(DontFretEntities context, string email, string fullName, string password)
        {
            ///new instance of the UserManager
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ///sets the user manager password validator
            UserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            ///creates the new user object
            var StoreManager = new ApplicationUser
            {
                UserName = email,
                FullName = fullName,
                Email = email,
            };

            ///creates a new user
            var result = UserManager.Create(StoreManager, password);

            ///validates the resul
            if (!result.Succeeded)
            {
                ///displays the errors seperated by a semicolon
                throw new Exception(string.Join(";", result.Errors));
            }
        }

        /// <summary>
        /// Applies a role to the newly created user at DB creation
        /// </summary>
        /// <param name="context"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        private void SetRoleToUser(DontFretEntities context, string email, string role)
        {

            ///new instance of the UserManager
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var User = context.Users.Where(u => u.Email == email).First();

            var result = UserManager.AddToRole(User.Id, role);

            ///validates the result
            if (!result.Succeeded)
            {
                ///displays the errors seperated by a semicolon
                throw new Exception(string.Join(";", result.Errors));
            }
        }


    }
}