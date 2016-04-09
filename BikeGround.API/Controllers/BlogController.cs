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
    public class BlogController : ApiControllerWithHub<NotificationHub>
    {
        private readonly SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        private readonly ISqlGenerator<Blog> _sqlGenerator = new SqlGenerator<Blog>();
        private long LogedUserID { get; set; }

        /// <summary>
        ///     Konstruktor, inicijalizira UserID
        /// </summary>
        public BlogController()
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
        [Route("api/blog/multiple"), HttpGet]
        public async Task<HttpResponseMessage> GetUserAll(string sinceId = null, string count = null)
        {
            IEnumerable<Blog> items;

            var _blogRepository = new BlogRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _blogRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _blogRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/blog/multiple");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        /// <summary>
        /// Dohvaća jedan jedini podatak preko UserID-a
        /// </summary>
        /// <returns>Objekt</returns>
        [AcceptVerbs("GET")]
        [Route("api/blog/single"), HttpGet]
        public async Task<HttpResponseMessage> GetUserSingle()
        {
            var item = new Blog();

            var _blogRepository = new BlogRepository(_sqlCon, _sqlGenerator);

            item = await _blogRepository.GetFirstAsync(new { UserID = this.LogedUserID });

            Debug.WriteLine("api/blog/single");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [Route("api/blog"), HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Blog obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _blogRepository = new BlogRepository(_sqlCon, _sqlGenerator);
                var ID = await _blogRepository.InsertAsync(obj);

                if (ID > 0)
                {
                    base.Broadcast_Info(obj.Name);

                    return Request.CreateResponse(HttpStatusCode.Created, ID);
                }

                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        [Route("api/blog/{id}"), HttpPut]
        public async Task<HttpResponseMessage> Put(long Id, [FromBody] Blog obj)
        {
            obj.UserID = this.LogedUserID;

            if (ModelState.IsValid)
            {
                var _blogRepository = new BlogRepository(_sqlCon, _sqlGenerator);

                obj.ID = Id;

                var item = await _blogRepository.UpdateAsync(obj);

                if (item)
                {
                    base.Broadcast_Info(obj.Name);

                    var msg = new HttpResponseMessage(HttpStatusCode.OK);
                    return msg;
                }
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public async Task<HttpResponseMessage> Delete(long Id)
        {
            var _blogRepository = new BlogRepository(_sqlCon, _sqlGenerator);
            var status = await _blogRepository.DeleteAsync(new { ID = Id, UserID = this.LogedUserID });

            if (status)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #endregion

        #region Logirani user tudi podaci

        [Route("api/blog"), HttpGet]
        public async Task<HttpResponseMessage> GetAll(string sinceId = null, string count = null)
        {
            IEnumerable<Blog> items;

            var _blogRepository = new BlogRepository(_sqlCon, _sqlGenerator);

            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))
            {
                items = await _blogRepository.GetWhereAsyncPaged(new { UserID = this.LogedUserID }, sinceId, count);
            }
            else
            {
                items = await _blogRepository.GetWhereAsync(new { UserID = this.LogedUserID });
            }

            if (items == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Debug.WriteLine("api/blog");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [AcceptVerbs("GET")]
        [Route("api/blog/{id}"), HttpGet]
        public async Task<HttpResponseMessage> GetSingle(long Id)
        {
            var item = new Blog();

            var _blogRepository = new BlogRepository(_sqlCon, _sqlGenerator);

            item = await _blogRepository.GetFirstAsync(new { ID = Id });

            if (item == null)
            {
                item = new Blog();
            }

            Debug.WriteLine("api/blog/{id}");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        #endregion
    }
}