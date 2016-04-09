using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNet.Identity.Dapper;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Configuration;

namespace BikeGround.API.Models
{
    public class AppMember : IdentityMember
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppMember, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ExternalBearer);
            // Add custom AppMember claims here
            return userIdentity;
        }
    }

    /// <summary>
    /// Create and opens a connection to a MSSql database
    /// </summary>

    public class ApplicationDbContext : DbManager
    {
        public ApplicationDbContext(string connectionName)
            : base(connectionName)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}