using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IAspNetUsersRepository : IDataRepository<AspNetUsers>
    {
        //IAspNetUsersRepository is inheriting all CRUD operations 
    }

    public class AspNetUsersRepository : DataRepository<AspNetUsers>, IAspNetUsersRepository
    {
        public AspNetUsersRepository(IDbConnection connection, ISqlGenerator<AspNetUsers> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        #region General

        public IEnumerable<AspNetUsers> GetPaged(int sinceId, int count)
        {
            IEnumerable<AspNetUsers> aspnetuserss = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                aspnetuserss = cn.Query<AspNetUsers>("SELECT TOP " + count + " * FROM AspNetUsers WHERE ID > @sinceId AND", dbArgs);
            }

            return aspnetuserss;
        }

        public async Task<IEnumerable<AspNetUsers>> GetPagedAsync(int sinceId, int count)
        {
            IEnumerable<AspNetUsers> aspnetuserss = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                aspnetuserss = await cn.QueryAsync<AspNetUsers>("SELECT TOP " + count + " * FROM AspNetUsers WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return aspnetuserss;
        }

        #endregion


        #region Administration

        public IEnumerable<AspNetUsers> GetPagedForAdmin(int sinceId, int count, long UserID)
        {
            IEnumerable<AspNetUsers> aspnetuserss = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                aspnetuserss = cn.Query<AspNetUsers>("SELECT TOP " + count + " * FROM AspNetUsers WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return aspnetuserss;
        }

        public async Task<IEnumerable<AspNetUsers>> GetPagedAsyncForAdmin(int sinceId, int count, long UserID)
        {
            IEnumerable<AspNetUsers> aspnetuserss = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                aspnetuserss = await cn.QueryAsync<AspNetUsers>("SELECT TOP " + count + " * FROM AspNetUsers WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return aspnetuserss;
        }

        #endregion
    }
}