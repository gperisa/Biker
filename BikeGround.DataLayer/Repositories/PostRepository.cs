using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IPostRepository : IDataRepository<Post>
    {
        //IPostRepository is inheriting all CRUD operations 
    }

    public class PostRepository : DataRepository<Post>, IPostRepository
    {
        public PostRepository(IDbConnection connection, ISqlGenerator<Post> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        #region General

        public IEnumerable<Post> GetPaged(int sinceId, int count)
        {
            IEnumerable<Post> posts = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                posts = cn.Query<Post>("SELECT TOP " + count + " * FROM Post WHERE ID > @sinceId", dbArgs);
            }

            return posts;
        }

        public async Task<IEnumerable<Post>> GetPagedAsync(int sinceId, int count)
        {
            IEnumerable<Post> posts = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                posts = await cn.QueryAsync<Post>("SELECT TOP " + count + " * FROM Post WHERE ID > @sinceId", dbArgs);
            }

            return posts;
        } 

        #endregion


        #region Administration

        public IEnumerable<Post> GetPagedForAdmin(int sinceId, int count, long userId)
        {
            IEnumerable<Post> posts = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", userId);

                posts = cn.Query<Post>("SELECT TOP " + count + " * FROM Post WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return posts;
        }

        public async Task<IEnumerable<Post>> GetPagedAsyncForAdmin(int sinceId, int count, long userId)
        {
            IEnumerable<Post> posts = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", userId);

                posts = await cn.QueryAsync<Post>("SELECT TOP " + count + " * FROM Post WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return posts;
        } 
        #endregion
    }
}