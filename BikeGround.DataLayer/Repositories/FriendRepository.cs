using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IFriendRepository : IDataRepository<Friend>
    {
        //IFriendRepository is inheriting all CRUD operations 
    }

    public class FriendRepository : DataRepository<Friend>, IFriendRepository
    {
        public FriendRepository(IDbConnection connection, ISqlGenerator<Friend> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}