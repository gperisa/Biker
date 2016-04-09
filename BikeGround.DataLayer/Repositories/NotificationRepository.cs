using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface INotificationRepository : IDataRepository<Notification>
    {
        //INotificationRepository is inheriting all CRUD operations 
    }

    public class NotificationRepository : DataRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(IDbConnection connection, ISqlGenerator<Notification> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}
