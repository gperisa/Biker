using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IMessagesRepository : IDataRepository<Messages>
    {
        //IMessagesRepository is inheriting all CRUD operations 
    }

    public class MessagesRepository : DataRepository<Messages>, IMessagesRepository
    {
        public MessagesRepository(IDbConnection connection, ISqlGenerator<Messages> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

    }
}