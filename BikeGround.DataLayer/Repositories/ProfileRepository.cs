using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IProfileRepository : IDataRepository<Profile>
    {
        //IProfileRepository is inheriting all CRUD operations 
    }

    public class ProfileRepository : DataRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(IDbConnection connection, ISqlGenerator<Profile> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }


        #region General

        public IEnumerable<Profile> GetPaged(int sinceId, int count)
        {
            IEnumerable<Profile> profiles = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                profiles = cn.Query<Profile>("SELECT TOP " + count + " * FROM Profile WHERE ID > @sinceId AND", dbArgs);
            }

            return profiles;
        }

        public async Task<IEnumerable<Profile>> GetPagedAsync(int sinceId, int count)
        {
            IEnumerable<Profile> profiles = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                profiles = await cn.QueryAsync<Profile>("SELECT TOP " + count + " * FROM Profile WHERE ID > @sinceId AND", dbArgs);
            }

            return profiles;
        }

        #endregion


        #region Administration

        public IEnumerable<Profile> GetPaged(int sinceId, int count, long UserID)
        {
            IEnumerable<Profile> profiles = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                profiles = cn.Query<Profile>("SELECT TOP " + count + " * FROM Profile WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return profiles;
        }

        public async Task<IEnumerable<Profile>> GetPagedAsync(int sinceId, int count, long UserID)
        {
            IEnumerable<Profile> profiles = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                profiles = await cn.QueryAsync<Profile>("SELECT TOP " + count + " * FROM Profile WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return profiles;
        } 

        #endregion
    }
}