using Google.Apis.Analytics.v3;
using Google.Apis.Services;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using BikeGround.API.Common;
using Google.Apis.Analytics.v3.Data;
using Google.Apis.Auth.OAuth2;
using AnalyticsService = Google.Apis.Analytics.v3.AnalyticsService;

namespace BikeGround.API.Controllers
{
    [System.Web.Http.Authorize]
    [EnableCors("http://localhost:3668", "*", "*")]
    public class AnalyticsController : ApiController
    {
        private long LogedUserID { get; set; }

        /// <summary>
        ///     Konstruktor, inicijalizira UserID
        /// </summary>
        public AnalyticsController()
        {
            this.LogedUserID = Helpers.GetUserIDFromClaims((ClaimsPrincipal)Thread.CurrentPrincipal);
        }

        #region Logirani user

        [Route("api/analytics/{dimension}"), HttpGet]
        public string GetStats(string dimension)
        {
            string response = "";

            switch (dimension)
            {
                case "date":
                    response = GetVisitsPerDay(profileId: "ga:92390767", startDate: "2014-10-01", endDate: "2015-10-31", metrics: "ga:visits");
                    break;
                case "country":
                    response = GetVisitsPerCountry(profileId: "ga:92390767", startDate: "2014-10-01", endDate: "2015-10-31", metrics: "ga:visits");
                    break;
                case "source":
                    response = GetVisitsPerSource(profileId: "ga:92390767", startDate: "2014-10-01", endDate: "2015-10-31", metrics: "ga:visits");
                    break;
                case "unique":
                    response = GetVisitsUniquePerDay(profileId: "ga:92390767", startDate: "2014-10-01", endDate: "2015-10-31", metrics: "ga:visits");
                    break;
            }

            return response;
        }

     
        public string GetVisitsPerDay(string profileId, string startDate, string endDate, string metrics)
        {
            var service = GAServiceInitialize();

            DataResource.GaResource.GetRequest request = service.Data.Ga.Get(profileId, startDate, endDate, metrics);
            request.Dimensions = "ga:month,ga:day,ga:pagePath";
            request.Filters = "ga:pagePath==/putovanja/";
            GaData data = request.Execute();

            var json = new JavaScriptSerializer().Serialize(data.Rows);

            return json;
        }

        public string GetVisitsPerCountry(string profileId, string startDate, string endDate, string metrics)
        {
            var service = GAServiceInitialize();

            DataResource.GaResource.GetRequest request = service.Data.Ga.Get(profileId, startDate, endDate, metrics);
            request.Dimensions = "ga:country";
            GaData data = request.Execute();

            var json = new JavaScriptSerializer().Serialize(data.Rows);

            return json;
        }

        public string GetVisitsPerSource(string profileId, string startDate, string endDate, string metrics)
        {
            var service = GAServiceInitialize();

            DataResource.GaResource.GetRequest request = service.Data.Ga.Get(profileId, startDate, endDate, metrics);
            request.Dimensions = "ga:source";
            GaData data = request.Execute();

            var json = new JavaScriptSerializer().Serialize(data.Rows);

            return json;
        }

        public string GetVisitsUniquePerDay(string profileId, string startDate, string endDate, string metrics)
        {
            var service = GAServiceInitialize();

            DataResource.GaResource.GetRequest request = service.Data.Ga.Get(profileId, startDate, endDate, metrics);
            request.Dimensions = "ga:uniquePageviews";
            GaData data = request.Execute();

            var json = new JavaScriptSerializer().Serialize(data.Rows);

            return json;
        }


        private static AnalyticsService GAServiceInitialize()
        {
            const string ServiceAccountUser = "769873456329-4ksmbn5lgml6l3qjvs71iot3ca23md57@developer.gserviceaccount.com";
            //AssertionFlowClient client = new AssertionFlowClient(
            //    GoogleAuthenticationServer.Description, new X509Certificate2(@"E:\DEVELOPMENT\37578976e2fc6c26471525d7bf67e162543322d1-privatekey.p12", "notasecret", X509KeyStorageFlags.Exportable))
            //{
            //    Scope = AnalyticsService.Scopes.AnalyticsReadonly.GetStringValue(),
            //    ServiceAccountId = ServiceAccountUser
            //};

            //var authenticator = new OAuth2Authenticator<AssertionFlowClient>(client, AssertionFlowClient.GetState);
            //var service = new AnalyticsService(new BaseClientService.Initializer()
            //{
            //    Authenticator = authenticator
            //});
            //return service;

            string[] scopes = new string[] { AnalyticsService.Scope.Analytics }; // view and manage your Google Analytics data

            var keyFilePath = @"E:\DEVELOPMENT\37578976e2fc6c26471525d7bf67e162543322d1-privatekey.p12";    // Downloaded from https://console.developers.google.com
            var serviceAccountEmail = "769873456329-4ksmbn5lgml6l3qjvs71iot3ca23md57@developer.gserviceaccount.com";  // found https://console.developers.google.com

            //loading the Key file
            var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);
            var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes
            }.FromCertificate(certificate));

            var service = new AnalyticsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Analytics API Sample",
            });
            return service;
        }

        #endregion
    }
}