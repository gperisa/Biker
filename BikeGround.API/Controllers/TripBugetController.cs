using BikeGround.API.Common;
using BikeGround.API.Controllers.Base;
using BikeGround.API.Hubs;
using BikeGround.DataLayer;
using BikeGround.DataLayer.Repositories;
using BikeGround.Models;
using MicroOrm.Pocos.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BikeGround.API.Controllers
{
    [Authorize]
    [EnableCors("http://localhost:3668", "*", "*")]
    public class TripBugetController : ApiControllerWithHub<NotificationHub>
    {
        private readonly SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        private readonly ISqlGenerator<TripBuget> _sqlGenerator = new SqlGenerator<TripBuget>();
        private long LogedUserID { get; set; }

        /// <summary>
        ///     Konstruktor, inicijalizira UserID
        /// </summary>
        public TripBugetController()
        {
            this.LogedUserID = Helpers.GetUserIDFromClaims((ClaimsPrincipal)Thread.CurrentPrincipal);
        }

        #region Logirani user

        /// <summary>
        /// Dohvaća listu podataka isključivo preko UserID-a
        /// </summary>
        /// <param name="sinceId">Od podatak</param>
        /// <param name="count">Veličina stranice</param>
        /// <returns>Listu objekata</returns>
        [Route("api/tripbuget/multiple"), HttpGet]
        public async Task<HttpResponseMessage> GetUserAll(string sinceId = null, string count = null)
        {
            IEnumerable<TripBuget> items;

            var _tripbugetRepository = new TripBugetRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _tripbugetRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _tripbugetRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/tripbuget/multiple");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Dohvaća jedan jedini podatak preko UserID-a
        /// </summary>
        /// <returns>Objekt</returns>
        [AcceptVerbs("GET")]
        [Route("api/tripbuget/single"), HttpGet]
        public async Task<HttpResponseMessage> GetUserSingle()
        {
            var item = new TripBuget();

            var _tripbugetRepository = new TripBugetRepository(_sqlCon, _sqlGenerator);

            item = await _tripbugetRepository.GetFirstAsync(new { UserID = this.LogedUserID });

            Debug.WriteLine("api/tripbuget/single");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [Route("api/tripbuget"), HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] TripBuget obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _tripbugetRepository = new TripBugetRepository(_sqlCon, _sqlGenerator);
                var ID = await _tripbugetRepository.InsertAsync(obj);

                if (ID > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, ID);
                }

                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [Route("api/tripbuget/{id}"), HttpPut]
        public async Task<HttpResponseMessage> Put(long Id, [FromBody] TripBuget obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _tripbugetRepository = new TripBugetRepository(_sqlCon, _sqlGenerator);

                obj.ID = Id;

                var item = await _tripbugetRepository.UpdateAsync(obj);

                if (item)
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.OK);
                    return msg;
                }
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public async Task<HttpResponseMessage> Delete(long Id)
        {
            var _tripbugetRepository = new TripBugetRepository(_sqlCon, _sqlGenerator);
            var status = await _tripbugetRepository.DeleteAsync(new { ID = Id, UserID = this.LogedUserID });

            if (status)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #endregion

        #region Logirani user tudi podaci

        [Route("api/tripbuget"), HttpGet]
        public async Task<HttpResponseMessage> GetAll(string sinceId = null, string count = null)
        {
            IEnumerable<TripBuget> items;

            var _tripbugetRepository = new TripBugetRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _tripbugetRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _tripbugetRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/tripbuget");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [AcceptVerbs("GET")]
        [Route("api/tripbuget/{id}"), HttpGet]
        public async Task<HttpResponseMessage> GetSingle(long Id)
        {
            var item = new TripBuget();

            var _tripbugetRepository = new TripBugetRepository(_sqlCon, _sqlGenerator);

            item = await _tripbugetRepository.GetFirstAsync(new { ID = Id });

            if (item == null)
            {
                item = new TripBuget();
            }

            Debug.WriteLine("api/tripbuget/{id}");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        #endregion
    }
}