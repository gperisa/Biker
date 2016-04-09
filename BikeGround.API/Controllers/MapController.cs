using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Xml;

namespace BikeGround.API.Controllers
{
    [System.Web.Http.Authorize]
    [EnableCors("http://localhost:3668", "*", "*")]
    public class MapController : ApiController
    {
        //// POST: api/Map
        ///// <summary>
        ///// Get elevation chart
        ///// </summary>
        ///// <param name="mqUrl">MapQuest URL to get elevation image</param>
        ///// <returns></returns>
        //[Route("api/map/elevation")]
        //[HttpPost]
        //public HttpResponseMessage GetElevationImage([FromBody]string[] mqUrl, string sessionId)
        //{    
        //    var latLngCollection = string.Join(",", mqUrl);
      
        //    byte[] data;
        //    using (var webClient = new WebClient())
        //        data = webClient.DownloadData("http://open.mapquestapi.com/elevation/v1/chart?key=Fmjtd|luurn9ur25%2C25%3Do5-9wz55a&inFormat=kvp&&sessionId=" + sessionId + "&shapeFormat=raw&width=725&height=350&unit=m");

        //    string enc = Convert.ToBase64String(data);
  
        //  return Request.CreateResponse(enc);
        //}

        // POST: api/Map
        /// <summary>
        /// Save .gpx route
        /// </summary>
        /// <param name="mqUrl">MapQuest URL to get elevation image</param>
        /// <returns></returns>
        [System.Web.Http.Route("api/map/route")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveGpx(object request)//  Waypoints waypoints)
        {
            var blup = JsonConvert.DeserializeObject<List<Waypoints>>(request.ToString());

            

            const string gpx = "http://www.topografix.com/GPX/1/1",
            xsi = "http://www.w3.org/20...Schema-instance";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("gpx", gpx);
            root.SetAttribute("version", "1.1");
            root.SetAttribute("creator", "Bikeground.com");
            XmlAttribute schemaLocation = xmlDoc.CreateAttribute("xsi", "schemaLocation", xsi);
            schemaLocation.Value =
            @"http://www.topografix.com/GPX/1/1 
            http://www.topografi...GPX/1/1/gpx.xsd
            http://www.topografi...X/gpx_style/0/2
            http://www.topografi...2/gpx_style.xsd
            http://www.topografi...gpx_overlay/0/3
            http://www.topografix.com/GPX/gpx_overlay/...gpx_overlay.xsd
            http://www.topografi...px_modified/0/1
            http://www.topografix.com/GPX/gpx_modified...px_modified.xsd";
            root.SetAttributeNode(schemaLocation);         

            XmlElement trk = xmlDoc.CreateElement("trk");
            XmlElement trkseg = xmlDoc.CreateElement("trkseg");
            XmlElement trkpt = xmlDoc.CreateElement("trkpt");

            trkseg.AppendChild(trkpt);
            trk.AppendChild(trkseg);
            root.AppendChild(trk);

            xmlDoc.AppendChild(root);

            return Request.CreateResponse(true);
        }   
   
        public class Person
        {
            public int PersonId { get; set; }
            public string Name { get; set; }
        }

        [Serializable]
        public class Waypoints
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
    }
}