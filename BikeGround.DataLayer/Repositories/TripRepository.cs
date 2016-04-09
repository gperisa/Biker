using System.Data;
using BikeGround.Models;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;

namespace BikeGround.DataLayer.Repositories
{
    internal interface ITripRepository : IDataRepository<Trip>
    {
        
    }

    public class TripRepository : DataRepository<Trip>, ITripRepository
    {
        public TripRepository(IDbConnection connection, ISqlGenerator<Trip> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}