using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IRouteRepository : IDataRepository<Route>
    {
        //IRouteRepository is inheriting all CRUD operations 
    }

    public class RouteRepository : DataRepository<Route>, IRouteRepository
    {
        public RouteRepository(IDbConnection connection, ISqlGenerator<Route> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        public IEnumerable<Route> GetPaged(int sinceId, int count, long UserID)
        {
            IEnumerable<Route> routes = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                routes = cn.Query<Route>("SELECT TOP " + count + " * FROM Route WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return routes;
        }

        public async Task<IEnumerable<Route>> GetPagedAsync(int sinceId, int count, long UserID)
        {
            IEnumerable<Route> routes = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                routes = await cn.QueryAsync<Route>("SELECT TOP " + count + " * FROM Route WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return routes;
        }
    }
}