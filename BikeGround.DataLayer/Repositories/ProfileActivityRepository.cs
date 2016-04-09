using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IProfileActivityRepository : IDataRepository<ProfileActivity>
    {
        //IProfileActivityRepository is inheriting all CRUD operations 
    }

    public class ProfileActivityRepository : DataRepository<ProfileActivity>, IProfileActivityRepository
    {
        public ProfileActivityRepository(IDbConnection connection, ISqlGenerator<ProfileActivity> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        #region General

        public IEnumerable<ProfileActivity> GetPaged(int sinceId, int count)
        {
            IEnumerable<ProfileActivity> profileactivitys = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                profileactivitys = cn.Query<ProfileActivity>("SELECT TOP " + count + " * FROM ProfileActivity WHERE ID > @sinceId", dbArgs);
            }

            return profileactivitys;
        }

        public async Task<IEnumerable<ProfileActivity>> GetPagedAsync(int sinceId, int count)
        {
            IEnumerable<ProfileActivity> profileactivitys = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                profileactivitys = await cn.QueryAsync<ProfileActivity>("SELECT TOP " + count + " * FROM ProfileActivity WHERE ID > @sinceId", dbArgs);
            }

            return profileactivitys;
        }

        #endregion


        #region Administration

        public IEnumerable<ProfileActivity> GetPagedForAdmin(int sinceId, int count, long UserID)
        {
            IEnumerable<ProfileActivity> profileactivitys = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                profileactivitys = cn.Query<ProfileActivity>("SELECT TOP " + count + " * FROM ProfileActivity WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return profileactivitys;
        }

        public async Task<IEnumerable<ProfileActivity>> GetPagedAsyncForAdmin(int sinceId, int count, long UserID)
        {
            IEnumerable<ProfileActivity> profileactivitys = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                profileactivitys = await cn.QueryAsync<ProfileActivity>("SELECT TOP " + count + " * FROM ProfileActivity WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return profileactivitys;
        } 

        #endregion
    }
}