using BikeGround.API.Common;
using BikeGround.DataLayer;
using BikeGround.DataLayer.Repositories;
using BikeGround.Models;
using Dapper;
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
    public class MailboxController : ApiController
    {
        private readonly SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        private readonly ISqlGenerator<Mailbox> _sqlGenerator = new SqlGenerator<Mailbox>();
        private long LogedUserID { get; set; }

        /// <summary>
        ///     Konstruktor, inicijalizira UserID
        /// </summary>
        public MailboxController()
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
        [Route("api/mailbox/multiple"), HttpGet]
        public async Task<HttpResponseMessage> GetUserAll(string sinceId = "1", string count = "10")
        {
            IEnumerable<Mailbox> items;

            var _mailboxRepository = new MailboxRepository(_sqlCon, _sqlGenerator);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ToID", this.LogedUserID);
            parameters.Add("SinceID", sinceId);
            parameters.Add("Count", count);

            //Test
            items =  await _mailboxRepository.CallStoredProcedure("GetMailbox", parameters);

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/mailbox/multiple");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [Route("api/mailbox/sent/multiple"), HttpGet]
        public async Task<HttpResponseMessage> GetSentMailboxForUser(string sinceId = "1", string count = "10")
        {
            IEnumerable<Mailbox> items;

            var _mailboxRepository = new MailboxRepository(_sqlCon, _sqlGenerator);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("FromID", this.LogedUserID);
            parameters.Add("SinceID", sinceId);
            parameters.Add("Count", count);

            items = await _mailboxRepository.CallStoredProcedure("GetMailboxSent", parameters);

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/mailbox/sent/multiple");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Dohvaća jedan jedini podatak preko UserID-a
        /// </summary>
        /// <returns>Objekt</returns>
        [AcceptVerbs("GET")]
        [Route("api/mailbox/single"), HttpGet]
        public async Task<HttpResponseMessage> GetUserSingle()
        {
            var item = new Mailbox();

            var _mailboxRepository = new MailboxRepository(_sqlCon, _sqlGenerator);

            item = await _mailboxRepository.GetFirstAsync(new { FromID = this.LogedUserID, ToID = this.LogedUserID },"OR");

            Debug.WriteLine("api/mailbox/single");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [Route("api/mailbox"), HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Mailbox obj)
        {
            // obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _mailboxRepository = new MailboxRepository(_sqlCon, _sqlGenerator);
                var ID = await _mailboxRepository.InsertAsync(obj);

                if (ID > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, ID);
                }

                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [Route("api/mailbox/{id}"), HttpPut]
        public async Task<HttpResponseMessage> Put(long Id, [FromBody] Mailbox obj)
        {
            // obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _mailboxRepository = new MailboxRepository(_sqlCon, _sqlGenerator);

                obj.ID = Id;

                var item = await _mailboxRepository.UpdateAsync(obj);

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
            var _mailboxRepository = new MailboxRepository(_sqlCon, _sqlGenerator);
            var status = await _mailboxRepository.DeleteAsync(new { ID = Id, UserID = this.LogedUserID });

            if (status)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #endregion

        #region Logirani user tudi podaci

        [Route("api/mailbox"), HttpGet]
        public async Task<HttpResponseMessage> GetAll(string sinceId = null, string count = null)
        {
            IEnumerable<Mailbox> items;

            var _mailboxRepository = new MailboxRepository(_sqlCon, _sqlGenerator);

           

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _mailboxRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count,"OR");
            }
            else
            {
                items = await _mailboxRepository.GetWhereAsync(new { UserID = this.LogedUserID }, "OR");
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/mailbox");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [AcceptVerbs("GET")]
        [Route("api/mailbox/{id}"), HttpGet]
        public async Task<HttpResponseMessage> GetSingle(long Id)
        {
            var item = new Mailbox();

            var _mailboxRepository = new MailboxRepository(_sqlCon, _sqlGenerator);

            item = await _mailboxRepository.GetFirstAsync(new { ID = Id }, "OR");

            if (item == null)
            {
                item = new Mailbox();
            }

            Debug.WriteLine("api/mailbox/{id}");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        #endregion
    }
}