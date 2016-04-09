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
    public class UserEquipmentController : ApiController
    {
        private readonly SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        private readonly ISqlGenerator<UserEquipment> _sqlGenerator = new SqlGenerator<UserEquipment>();
        private long LogedUserID { get; set; }

        /// <summary>
        ///     Konstruktor, inicijalizira UserID
        /// </summary>
        public UserEquipmentController()
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
        [Route("api/userequipment/multiple"), HttpGet]
        public async Task<HttpResponseMessage> GetUserAll(string sinceId = null, string count = null)
        {
            IEnumerable<UserEquipment> items;

            var _userequipmentRepository = new UserEquipmentRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _userequipmentRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _userequipmentRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/userequipment/multiple");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Dohvaća jedan jedini podatak preko UserID-a
        /// </summary>
        /// <returns>Objekt</returns>
        [AcceptVerbs("GET")]
        [Route("api/userequipment/single"), HttpGet]
        public async Task<HttpResponseMessage> GetUserSingle()
        {
            var item = new UserEquipment();

            var _userequipmentRepository = new UserEquipmentRepository(_sqlCon, _sqlGenerator);

            item = await _userequipmentRepository.GetFirstAsync(new { UserID = this.LogedUserID });

            Debug.WriteLine("api/userequipment/single");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [Route("api/userequipment"), HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] UserEquipment obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _userequipmentRepository = new UserEquipmentRepository(_sqlCon, _sqlGenerator);
                var ID = await _userequipmentRepository.InsertAsync(obj);

                if (ID > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, ID);
                }

                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [Route("api/userequipment/{id}"), HttpPut]
        public async Task<HttpResponseMessage> Put(long Id, [FromBody] UserEquipment obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _userequipmentRepository = new UserEquipmentRepository(_sqlCon, _sqlGenerator);

                obj.ID = Id;

                var item = await _userequipmentRepository.UpdateAsync(obj);

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
            var _userequipmentRepository = new UserEquipmentRepository(_sqlCon, _sqlGenerator);
            var status = await _userequipmentRepository.DeleteAsync(new { ID = Id, UserID = this.LogedUserID });

            if (status)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #endregion

        #region Logirani user tudi podaci

        [Route("api/userequipment"), HttpGet]
        public async Task<HttpResponseMessage> GetAll(string sinceId = null, string count = null)
        {
            IEnumerable<UserEquipment> items;

            var _userequipmentRepository = new UserEquipmentRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _userequipmentRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _userequipmentRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/userequipment");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [AcceptVerbs("GET")]
        [Route("api/userequipment/{id}"), HttpGet]
        public async Task<HttpResponseMessage> GetSingle(long Id)
        {
            var item = new UserEquipment();

            var _userequipmentRepository = new UserEquipmentRepository(_sqlCon, _sqlGenerator);

            item = await _userequipmentRepository.GetFirstAsync(new { ID = Id });

            if (item == null)
            {
                item = new UserEquipment();
            }

            Debug.WriteLine("api/userequipment/{id}");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        #endregion
    }
}