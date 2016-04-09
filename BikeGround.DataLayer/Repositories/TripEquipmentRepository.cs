using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface ITripEquipmentRepository : IDataRepository<TripEquipment>
    {
        //ITripEquipmentRepository is inheriting all CRUD operations 
    }

    public class TripEquipmentRepository : DataRepository<TripEquipment>, ITripEquipmentRepository
    {
        public TripEquipmentRepository(IDbConnection connection, ISqlGenerator<TripEquipment> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}