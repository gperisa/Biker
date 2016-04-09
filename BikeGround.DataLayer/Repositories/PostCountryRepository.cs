using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IPostCountryRepository : IDataRepository<PostCountry>
    {
        //IPostCountryRepository is inheriting all CRUD operations 
    }

    public class PostCountryRepository : DataRepository<PostCountry>, IPostCountryRepository
    {
        public PostCountryRepository(IDbConnection connection, ISqlGenerator<PostCountry> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}