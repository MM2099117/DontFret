using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DFShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// User properties
        /// </summary>
        public string FullName { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string Postcode { get; set; }
        public string Town { get; set; }
        public string ContactNumber { get; set; }
        public bool isLoyaltyCardHolder { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}