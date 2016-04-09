using BikeGround.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Bikeground.API.Exceptions;

namespace BikeGround.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var actionContext = actionExecutedContext.ActionContext;

            if (actionExecutedContext.Exception is NotImplementedException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else if (actionExecutedContext.Exception is DatabaseException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

                Logger.Log.LogServiceRequest(actionContext.RequestContext.Principal.Identity.Name, actionContext.ActionDescriptor.ActionName + " - EXCEPTION", 1,
                Helpers.GetClientIp(HttpContext.Current.Request.Headers["X-Forwarded-For"],
                    HttpContext.Current.Request.UserHostAddress), "", actionContext.Request.RequestUri.ToString(), actionExecutedContext.Exception.Message);
            }
            else if (actionExecutedContext.Exception is AuthorizationException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                Logger.Log.LogServiceRequest(actionContext.RequestContext.Principal.Identity.Name, actionContext.ActionDescriptor.ActionName + " - EXCEPTION", 1,
                Helpers.GetClientIp(HttpContext.Current.Request.Headers["X-Forwarded-For"],
                    HttpContext.Current.Request.UserHostAddress), "", actionContext.Request.RequestUri.ToString(), actionExecutedContext.Exception.Message);
            }
            else if (actionExecutedContext.Exception is Exception)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

                Logger.Log.LogServiceRequest(actionContext.RequestContext.Principal.Identity.Name, actionContext.ActionDescriptor.ActionName + " - EXCEPTION", 1,
                Helpers.GetClientIp(HttpContext.Current.Request.Headers["X-Forwarded-For"],
                    HttpContext.Current.Request.UserHostAddress), "", actionContext.Request.RequestUri.ToString(), actionExecutedContext.Exception.Message);
            }
        }
    }
}