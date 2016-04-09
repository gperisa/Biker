using BikeGround.API;
using BikeGround.API.Filters;
using System;
using System.Diagnostics;
using System.Web.Http;

namespace BikeGround.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(Object source, EventArgs e)
        {
#if DEBUG
            Debug.WriteLine("No SSL");
#else
                if (!Context.Request.IsSecureConnection)
                {
                    Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
                }
#endif
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
                       
        }
    }
}
