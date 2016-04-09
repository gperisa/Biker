using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BikeGround.API.Identity
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Url.Request.RequestUri.AbsoluteUri == "http://localhost:3668/api/login")
            {
                return;
            }

            if (!actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                this.HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}