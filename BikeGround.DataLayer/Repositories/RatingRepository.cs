using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IRatingRepository : IDataRepository<Rating>
    {
        //IRatingRepository is inheriting all CRUD operations 
    }

    public class RatingRepository : DataRepository<Rating>, IRatingRepository
    {
        public RatingRepository(IDbConnection connection, ISqlGenerator<Rating> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        public IEnumerable<Rating> GetPaged(int sinceId, int count, long UserID)
        {
            IEnumerable<Rating> ratings = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                ratings = cn.Query<Rating>("SELECT TOP " + count + " * FROM Rating WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return ratings;
        }

        public async Task<IEnumerable<Rating>> GetPagedAsync(int sinceId, int count, long UserID)
        {
            IEnumerable<Rating> ratings = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                ratings = await cn.QueryAsync<Rating>("SELECT TOP " + count + " * FROM Rating WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return ratings;
        }
    }
}