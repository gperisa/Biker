using BikeGround.API.Identity;
using BikeGround.DataLayer;
using BikeGround.DataLayer.Repositories;
using BikeGround.Models;
using MicroOrm.Pocos.SqlGenerator;
using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BikeGround.API.Controllers
{
    public abstract class BaseController<T> : ApiController where T : class 
    {
        public abstract Task<HttpResponseMessage> Get([FromUri] int sinceId, [FromUri] int count);

        public abstract Task<HttpResponseMessage> Get();

        public abstract Task<HttpResponseMessage> Get(long id);

        public abstract Task<HttpResponseMessage> Post([FromBody]T entity);

        public abstract Task<HttpResponseMessage> Put(long id, [FromBody]T entity);

        public abstract Task<HttpResponseMessage> Delete(long id);
    }
}