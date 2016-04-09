using AspNet.Identity.Dapper;
using BikeGround.API.Models;
using BikeGround.DataLayer;
using MicroOrm.Pocos.SqlGenerator;
using Microsoft.AspNet.Identity.Owin;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BikeGround.API.Controllers
{
    [EnableCors("http://localhost:3668", "*", "*")]
    public class LoginController : ApiController
    {
        SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        ISqlGenerator<User> _sqlGenerator = new SqlGenerator<User>();

        // POST: api/User
        public async Task<HttpResponseMessage> Post([FromBody]User User)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var context = Request.GetOwinContext();
                    var manager = new ApplicationUserManager(new UserStore<AppMember>(context.Get<ApplicationDbContext>() as DbManager));

                    var appMember = new AppMember { UserName = User.UserName, Email = User.UserName };
                    var result = await manager.CreateAsync(appMember, User.Password);

                    var response = new HttpResponseMessage(HttpStatusCode.Created);
                    return response;
                }

                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
