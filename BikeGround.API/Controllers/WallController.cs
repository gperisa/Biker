using BikeGround.API.Common;
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
    public class WallController : ApiController
    {
        private readonly SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        private readonly ISqlGenerator<Wall> _sqlGenerator = new SqlGenerator<Wall>();
        private long LogedUserID { get; set; }

        /// <summary>
        ///     Konstruktor, inicijalizira UserID
        /// </summary>
        public WallController()
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
        [Route("api/wall/multiple"), HttpGet]
        public async Task<HttpResponseMessage> GetUserAll(string sinceId = null, string count = null)
        {
            IEnumerable<Wall> items;

            var _wallRepository = new WallRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _wallRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _wallRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/wall/multiple");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Dohvaća jedan jedini podatak preko UserID-a
        /// </summary>
        /// <returns>Objekt</returns>
        [AcceptVerbs("GET")]
        [Route("api/wall/single"), HttpGet]
        public async Task<HttpResponseMessage> GetUserSingle()
        {
            var item = new Wall();

            var _wallRepository = new WallRepository(_sqlCon, _sqlGenerator);

            item = await _wallRepository.GetFirstAsync(new { UserID = this.LogedUserID });

            Debug.WriteLine("api/wall/single");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [Route("api/wall"), HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Wall obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _wallRepository = new WallRepository(_sqlCon, _sqlGenerator);
                var ID = await _wallRepository.InsertAsync(obj);

                if (ID > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, ID);
                }

                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [Route("api/wall/{id}"), HttpPut]
        public async Task<HttpResponseMessage> Put(long Id, [FromBody] Wall obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _wallRepository = new WallRepository(_sqlCon, _sqlGenerator);

                obj.ID = Id;

                var item = await _wallRepository.UpdateAsync(obj);

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
            var _wallRepository = new WallRepository(_sqlCon, _sqlGenerator);
            var status = await _wallRepository.DeleteAsync(new { ID = Id, UserID = this.LogedUserID });

            if (status)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #endregion

        #region Logirani user tudi podaci

        [Route("api/wall"), HttpGet]
        public async Task<HttpResponseMessage> GetAll(string sinceId = null, string count = null)
        {
            IEnumerable<Wall> items;

            var _wallRepository = new WallRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _wallRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _wallRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/wall");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [AcceptVerbs("GET")]
        [Route("api/wall/{id}"), HttpGet]
        public async Task<HttpResponseMessage> GetSingle(long Id)
        {
            var item = new Wall();

            var _wallRepository = new WallRepository(_sqlCon, _sqlGenerator);

            item = await _wallRepository.GetFirstAsync(new { ID = Id });

            if (item == null)
            {
                item = new Wall();
            }

            Debug.WriteLine("api/wall/{id}");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        #endregion
    }
}