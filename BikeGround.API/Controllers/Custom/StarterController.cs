using BikeGround.DataLayer;
using BikeGround.DataLayer.Repositories;
using BikeGround.Models;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Linq;
using BikeGround.API.Common;
using System.Security.Claims;
using System.Threading;

namespace BikeGround.API.Controllers
{
    [Authorize]
    [EnableCors("http://localhost:3668", "*", "*")]
    public class StarterController : ApiController
    {
        SqlConnection sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());
        ISqlGenerator<Blog> sqlGenerator = new SqlGenerator<Blog>();

        private long LogedUserID = 0;

        /// <summary>
        /// Konstruktor, inicijalizira UserID
        /// </summary>
        public StarterController()
        {
            this.LogedUserID = Helpers.GetUserIDFromClaims((ClaimsPrincipal)Thread.CurrentPrincipal);
        }

        [Route("api/starter/init"), HttpGet]
        public async Task<Starter> GetInitiator()
        {
            Starter s = new Starter();

            KeyValuePairRepository _keyValuePairRepository = new KeyValuePairRepository(sqlCon);
            SinglePropertyRepository _singlePropertyRepository = new SinglePropertyRepository(sqlCon);
            BlogRepository _blogRepository = new BlogRepository(sqlCon, new SqlGenerator<Blog>());

            s.Blog = await _blogRepository.GetFirstAsync(new { UserID = this.LogedUserID });
            s.Trips = await _keyValuePairRepository.GetKeyValuePairAsync<Trip>(m => m.ID, m => m.Title, this.LogedUserID);
            s.Countries = await _keyValuePairRepository.GetKeyValuePairAsync<Country>(m => m.ID, m => m.Name);
            s.ChatActivity = await _singlePropertyRepository.GetSinglePropertyAsync<Profile>(m => m.ChatActivity, this.LogedUserID);
            s.BlogName = await _singlePropertyRepository.GetSinglePropertyAsync<Blog>(m => m.BlogName, this.LogedUserID);

            s.ProfileActivities = await _keyValuePairRepository.GetKeyValuePairAsync<ProfileActivity>(m => m.ID, m => m.ActivityType);

            return s;
        }
    }
}