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
    public class CommentController : ApiControllerWithHub<NotificationHub>
    {
        private readonly SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        private readonly ISqlGenerator<Comment> _sqlGenerator = new SqlGenerator<Comment>();
        private long LogedUserID { get; set; }

        /// <summary>
        ///     Konstruktor, inicijalizira UserID
        /// </summary>
        public CommentController()
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
        [Route("api/comment/multiple"), HttpGet]
        //[ResponseType(typeof(IEnumerable<Comment>))]
        public async Task<HttpResponseMessage> GetUserAll(string sinceId = null, string count = null)
        {
            IEnumerable<Comment> items;

            var _commentRepository = new CommentRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _commentRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _commentRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/comment/multiple");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Dohvaća jedan jedini podatak preko UserID-a
        /// </summary>
        /// <returns>Objekt</returns>
        [AcceptVerbs("GET")]
        [Route("api/comment/single"), HttpGet]
        //[ResponseType(typeof(Comment))]
        public async Task<HttpResponseMessage> GetUserSingle()
        {
            var item = new Comment();

            var _commentRepository = new CommentRepository(_sqlCon, _sqlGenerator);

            item = await _commentRepository.GetFirstAsync(new { UserID = this.LogedUserID });

            Debug.WriteLine("api/comment/single");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [Route("api/comment"), HttpPost]
        //[ResponseType(typeof(long))]
        public async Task<HttpResponseMessage> Post([FromBody] Comment obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _commentRepository = new CommentRepository(_sqlCon, _sqlGenerator);
                var ID = await _commentRepository.InsertAsync(obj);

                if (ID > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, ID);
                }

                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [Route("api/comment/{id}"), HttpPut]
        public async Task<HttpResponseMessage> Put(long Id, [FromBody] Comment obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _commentRepository = new CommentRepository(_sqlCon, _sqlGenerator);

                obj.ID = Id;

                var item = await _commentRepository.UpdateAsync(obj);

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
            var _commentRepository = new CommentRepository(_sqlCon, _sqlGenerator);
            var status = await _commentRepository.DeleteAsync(new { ID = Id, UserID = this.LogedUserID });

            if (status)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #endregion

        #region Logirani user tudi podaci

        [Route("api/comments/{id}"), HttpGet]
        public async Task<HttpResponseMessage> GetAll(long Id, string sinceId = null, string count = null)
        {
            IEnumerable<Comment> items;

            var _commentRepository = new CommentRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _commentRepository.GetWhereAsyncPaged(new { PostID = Id }, sinceId, count);
            }
            else
            {
                items = await _commentRepository.GetWhereAsync(new { PostID = Id });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/comments");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [AcceptVerbs("GET")]
        [Route("api/comment/{id}"), HttpGet]
        public async Task<HttpResponseMessage> GetSingle(long Id)
        {
            var item = new Comment();

            var _commentRepository = new CommentRepository(_sqlCon, _sqlGenerator);

            item = await _commentRepository.GetFirstAsync(new { ID = Id });

            if (item == null)
            {
                item = new Comment();
            }

            Debug.WriteLine("api/comment/{id}");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        #endregion
    }
}