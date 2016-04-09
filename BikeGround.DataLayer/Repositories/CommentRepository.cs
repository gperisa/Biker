
using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface ICommentRepository : IDataRepository<Comment>
    {
        //ICommentRepository is inheriting all CRUD operations 
    }

    public class CommentRepository : DataRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbConnection connection, ISqlGenerator<Comment> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        public IEnumerable<Comment> GetPaged(int sinceId, int count, long UserID)
        {
            IEnumerable<Comment> comments = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                comments = cn.Query<Comment>("SELECT TOP " + count + " * FROM Comment WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetPagedAsync(int sinceId, int count, long UserID)
        {
            IEnumerable<Comment> comments = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                comments = await cn.QueryAsync<Comment>("SELECT TOP " + count + " * FROM Comment WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return comments;
        }
    }
}