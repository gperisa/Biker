using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IUserEquipmentRepository : IDataRepository<UserEquipment>
    {
        //IUserEquipmentRepository is inheriting all CRUD operations 
    }

    public class UserEquipmentRepository : DataRepository<UserEquipment>, IUserEquipmentRepository
    {
        public UserEquipmentRepository(IDbConnection connection, ISqlGenerator<UserEquipment> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}