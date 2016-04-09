using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IPostUsersRepository : IDataRepository<PostUsers>
    {
        //IPostUsersRepository is inheriting all CRUD operations 
    }

    public class PostUsersRepository : DataRepository<PostUsers>, IPostUsersRepository
    {
        public PostUsersRepository(IDbConnection connection, ISqlGenerator<PostUsers> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}