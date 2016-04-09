using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IEquipmentCategoryRepository : IDataRepository<EquipmentCategory>
    {
        //IEquipmentCategoryRepository is inheriting all CRUD operations 
    }

    public class EquipmentCategoryRepository : DataRepository<EquipmentCategory>, IEquipmentCategoryRepository
    {
        public EquipmentCategoryRepository(IDbConnection connection, ISqlGenerator<EquipmentCategory> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        public IEnumerable<EquipmentCategory> GetPaged(int sinceId, int count, long UserID)
        {
            IEnumerable<EquipmentCategory> equipmentcategorys = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                equipmentcategorys = cn.Query<EquipmentCategory>("SELECT TOP " + count + " * FROM EquipmentCategory WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return equipmentcategorys;
        }

        public async Task<IEnumerable<EquipmentCategory>> GetPagedAsync(int sinceId, int count, long UserID)
        {
            IEnumerable<EquipmentCategory> equipmentcategorys = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                equipmentcategorys = await cn.QueryAsync<EquipmentCategory>("SELECT TOP " + count + " * FROM EquipmentCategory WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return equipmentcategorys;
        }
    }
}