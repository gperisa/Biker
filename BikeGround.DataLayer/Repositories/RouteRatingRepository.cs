using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IRouteRatingRepository : IDataRepository<RouteRating>
    {
        //IRouteRatingRepository is inheriting all CRUD operations 
    }

    public class RouteRatingRepository : DataRepository<RouteRating>, IRouteRatingRepository
    {
        public RouteRatingRepository(IDbConnection connection, ISqlGenerator<RouteRating> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        public IEnumerable<RouteRating> GetPaged(int sinceId, int count, long UserID)
        {
            IEnumerable<RouteRating> routeratings = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                routeratings = cn.Query<RouteRating>("SELECT TOP " + count + " * FROM RouteRating WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return routeratings;
        }

        public async Task<IEnumerable<RouteRating>> GetPagedAsync(int sinceId, int count, long UserID)
        {
            IEnumerable<RouteRating> routeratings = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                routeratings = await cn.QueryAsync<RouteRating>("SELECT TOP " + count + " * FROM RouteRating WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return routeratings;
        }
    }
}