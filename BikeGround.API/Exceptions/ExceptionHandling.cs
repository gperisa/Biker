using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

public class ApiExceptionAttribute : ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext context)
    {
        Exception e = context.Exception;
        Debug.WriteLine(e.Message);
        var request = context.ActionContext.Request;

        var response = new
        {
            Status = 400,
            Text = "Error just happened !"
        };

        context.Response = request.CreateResponse(HttpStatusCode.BadRequest, response);
    }
}