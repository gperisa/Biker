using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IFavoriteRouteRepository : IDataRepository<FavoriteRoute>
    {
        //IFavoriteRouteRepository is inheriting all CRUD operations 
    }

    public class FavoriteRouteRepository : DataRepository<FavoriteRoute>, IFavoriteRouteRepository
    {
        public FavoriteRouteRepository(IDbConnection connection, ISqlGenerator<FavoriteRoute> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}