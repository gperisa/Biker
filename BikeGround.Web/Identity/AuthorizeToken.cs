using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BikeGround.Web.Identity
{
    public class AuthorizeToken : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Headers.GetValues("access_token") == null)
            {
                return false;
            }

            string token = httpContext.Request.Headers.GetValues("access_token").FirstOrDefault();

            if (String.IsNullOrEmpty(token))
            {
                return false;
            }

            return ValidateToken(token);

            //return base.AuthorizeCore(httpContext);
        }

        private bool ValidateToken(string token)
        {
            if (token == "lambada")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            base.HandleUnauthorizedRequest(context);
        }
    }
}