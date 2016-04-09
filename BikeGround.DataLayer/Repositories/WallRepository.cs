using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IWallRepository : IDataRepository<Wall>
    {
        //IWallRepository is inheriting all CRUD operations 
    }

    public class WallRepository : DataRepository<Wall>, IWallRepository
    {
        public WallRepository(IDbConnection connection, ISqlGenerator<Wall> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        #region General

        public IEnumerable<Wall> GetPaged(int sinceId, int count)
        {
            IEnumerable<Wall> walls = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                walls = cn.Query<Wall>("SELECT TOP " + count + " * FROM Wall WHERE ID > @sinceId", dbArgs);
            }

            return walls;
        }

        public async Task<IEnumerable<Wall>> GetPagedAsync(int sinceId, int count)
        {
            IEnumerable<Wall> walls = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                walls = await cn.QueryAsync<Wall>("SELECT TOP " + count + " * FROM Wall WHERE ID > @sinceId", dbArgs);
            }

            return walls;
        }

        #endregion

        #region Administration

        public IEnumerable<Wall> GetPagedForAdmin(int sinceId, int count, long UserID)
        {
            IEnumerable<Wall> walls = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                walls = cn.Query<Wall>("SELECT TOP " + count + " * FROM Wall WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return walls;
        }

        public async Task<IEnumerable<Wall>> GetPagedAsyncForAdmin(int sinceId, int count, long UserID)
        {
            IEnumerable<Wall> walls = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                walls = await cn.QueryAsync<Wall>("SELECT TOP " + count + " * FROM Wall WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return walls;
        } 

        #endregion
    }
}