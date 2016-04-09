using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface ICountryRepository : IDataRepository<Country>
    {
        //ICountryRepository is inheriting all CRUD operations 
    }

    public class CountryRepository : DataRepository<Country>, ICountryRepository
    {
        public CountryRepository(IDbConnection connection, ISqlGenerator<Country> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        public IEnumerable<Country> GetPaged(int sinceId, int count, long UserID)
        {
            IEnumerable<Country> countrys = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                countrys = cn.Query<Country>("SELECT TOP " + count + " * FROM Country WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return countrys;
        }

        public async Task<IEnumerable<Country>> GetPagedAsync(int sinceId, int count, long UserID)
        {
            IEnumerable<Country> countrys = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                countrys = await cn.QueryAsync<Country>("SELECT TOP " + count + " * FROM Country WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return countrys;
        }
    }
}