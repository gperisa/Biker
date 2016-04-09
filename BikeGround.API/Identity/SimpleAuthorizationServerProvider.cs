using BikeGround.API.Common;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BikeGround.API.Identity
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly UserManager<User> UserManager;

        public SimpleAuthorizationServerProvider()
            : this(new UserManager<User>(new UserStore()))
        {
        }

        public SimpleAuthorizationServerProvider(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userData = await context.Request.ReadFormAsync();

            var user = await UserManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            // Roles are set here
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            // Here goes roles and rights check
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            //identity.AddClaim(new Claim(ClaimTypes.Sid, Helpers.GetUserIDFromDB(user.Email).ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));

            context.Validated(identity);

        }
    }
}