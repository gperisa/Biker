using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IEquipmentRepository : IDataRepository<Equipment>
    {
        //IEquipmentRepository is inheriting all CRUD operations 
    }

    public class EquipmentRepository : DataRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(IDbConnection connection, ISqlGenerator<Equipment> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}