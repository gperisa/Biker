using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IBlogRepository : IDataRepository<Blog>
    {
        //IBlogRepository is inheriting all CRUD operations 
    }

    public class BlogRepository : DataRepository<Blog>, IBlogRepository
    {
        public BlogRepository(IDbConnection connection, ISqlGenerator<Blog> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        #region General

        public IEnumerable<Blog> GetPaged(int sinceId, int count)
        {
            IEnumerable<Blog> blogs = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                blogs = cn.Query<Blog>("SELECT TOP " + count + " * FROM Blog WHERE ID > @sinceId", dbArgs);
            }

            return blogs;
        }

        public async Task<IEnumerable<Blog>> GetPagedAsync(int sinceId, int count)
        {
            IEnumerable<Blog> blogs = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);

                blogs = await cn.QueryAsync<Blog>("SELECT TOP " + count + " * FROM Blog WHERE ID > @sinceId", dbArgs);
            }

            return blogs;
        }

        public async Task<IEnumerable<Blog>> GetPagedAsyncForAdmin(int sinceId, int count, long userId)
        {
            IEnumerable<Blog> blogs = null;

            using (IDbConnection cn = Connection)
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("userId", sinceId);

                blogs = await cn.QueryAsync<Blog>("SELECT TOP " + count + " * FROM Blog WHERE ID > @sinceId AND USERID = @userId", dbArgs);
            }

            return blogs;
        }

        #endregion
    }
}