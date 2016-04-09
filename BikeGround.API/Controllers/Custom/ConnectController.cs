using BikeGround.API.Common;
using BikeGround.DataLayer;
using BikeGround.DataLayer.Repositories;
using BikeGround.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BikeGround.API.Controllers
{
    [Authorize]
    [EnableCors("http://localhost:3668", "*", "*")]
    public class ConnectController : ApiController
    {
        private readonly SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        private long LogedUserID { get; set; }

        /// <summary>
        ///     Konstruktor, inicijalizira UserID
        /// </summary>
        public ConnectController()
        {
            this.LogedUserID = Helpers.GetUserIDFromClaims((ClaimsPrincipal)Thread.CurrentPrincipal);
        }

        #region Logirani user

        [Route("api/connect"), HttpPost]
        public async Task<IEnumerable<Connect>> Post([FromBody] Connect obj)
        {
            if (obj == null)
            {
                return null;
            }

            obj.Accommodation = true;
            var _connectrepository = new ConnectRepository(_sqlCon);
            var items = await _connectrepository.GetPaged(obj, this.LogedUserID);

            return items;
        }

        #endregion
    }
}