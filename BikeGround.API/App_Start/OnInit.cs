using System;
using System.Configuration;

namespace BikeGround.API
{
    public sealed class AppInit
    {
        private static readonly Lazy<AppInit> lazy = new Lazy<AppInit>(() => new AppInit());

        public readonly bool Authorize;
        public readonly bool IsCaching;
        public readonly bool IsTracing;
        public readonly bool IsLogging;
        public readonly string ConnectionString;

        private AppInit()
        {
            IsCaching = ConfigurationManager.AppSettings["isCaching"] == "true" ? true : false;
            Authorize = ConfigurationManager.AppSettings["authorize"] == "true" ? true : false;
            IsTracing = ConfigurationManager.AppSettings["isTracing"] == "true" ? true : false;
            IsLogging = ConfigurationManager.AppSettings["isLogging"] == "true" ? true : false;
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static AppInit Instance
        {
            get { return lazy.Value; }
        }
    }
}