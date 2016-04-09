using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IMailboxRepository : IDataRepository<Mailbox>
    {
        //IMailboxRepository is inheriting all CRUD operations 
    }

    public class MailboxRepository : DataRepository<Mailbox>, IMailboxRepository
    {
        public MailboxRepository(IDbConnection connection, ISqlGenerator<Mailbox> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}