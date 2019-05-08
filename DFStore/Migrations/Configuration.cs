namespace DFStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DFStore.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<DFStore.Models.DontFretDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DontFretDBContext context)
        {
            if (!context.Roles.Any())
            {
                this.CreateRole(context, "Store Manager");
                this.CreateRole(context, "Assistant Manager");
                this.CreateRole(context, "Invoices Clerk");
                this.CreateRole(context, "Sales Assistant");
                this.CreateRole(context, "Customer");

            }
        }

        private void CreateRole(DontFretDBContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var result = roleManager.Create(new IdentityRole(roleName));

            if(!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }

        private void CreateUser(DontFretDBContext context, string email, string fullName, string password,
            string add1, string add2, string postcode, string town)
        {
            var userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(context));

            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false
            };

        //    var admin = new ApplicationUser
       //     {
        //        UserName = email,
                //FullName 
//};

        }
        
    }
}
