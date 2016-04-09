using BikeGround.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BikeGround.API.Filters
{
    public class LoggerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var actionContext = actionExecutedContext.ActionContext;

            Logger.Log.LogServiceRequest(actionContext.RequestContext.Principal.Identity.Name, actionContext.ActionDescriptor.ActionName + " - SUCCESS", 1,
                 Helpers.GetClientIp(HttpContext.Current.Request.Headers["X-Forwarded-For"],
                     HttpContext.Current.Request.UserHostAddress), "", actionContext.Request.RequestUri.ToString(), "");
        }
    }

    public class TraceFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controllerCtx = actionContext.ControllerContext;
            Logger.Tracing.Trace(
                 string.Format(
                     "Username:{2} " + DateTime.Now + " Controller {0}, Action {1} is starting...  queryString = {3}",
                     controllerCtx.ControllerDescriptor.ControllerName, actionContext.ActionDescriptor.ActionName,
                     actionContext.RequestContext.Principal.Identity.Name, actionContext.Request.RequestUri));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var actionContext = actionExecutedContext.ActionContext;

            Logger.Tracing.Trace(string.Format("Username:{1} " + DateTime.Now + " Action {0} ended...",
             actionContext.ActionDescriptor.ActionName,
             actionContext.RequestContext.Principal.Identity.Name));
        }
    }
}