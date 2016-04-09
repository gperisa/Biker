using System.Web.Http;
using System.Web.Http.Cors;
using BikeGround.API.Filters;
using BikeGround.API.Identity;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;

namespace BikeGround.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            config.Filters.Add(new ExceptionFilter());

            if (AppInit.Instance.IsLogging)
            {
                config.Filters.Add(new LoggerFilter());
            }

            if (AppInit.Instance.Authorize)
            {
                config.Filters.Add(new CustomAuthorization());
            }

            if (AppInit.Instance.IsTracing)
            {
                config.Filters.Add(new TraceFilter());
            }
        }
    }
}