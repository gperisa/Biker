using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface ITripBugetRepository : IDataRepository<TripBuget>
    {
        //ITripBugetRepository is inheriting all CRUD operations 
    }

    public class TripBugetRepository : DataRepository<TripBuget>, ITripBugetRepository
    {
        public TripBugetRepository(IDbConnection connection, ISqlGenerator<TripBuget> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}