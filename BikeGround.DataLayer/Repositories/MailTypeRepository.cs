using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface IMailTypeRepository : IDataRepository<MailType>
    {
        //IMailTypeRepository is inheriting all CRUD operations 
    }

    public class MailTypeRepository : DataRepository<MailType>, IMailTypeRepository
    {
        public MailTypeRepository(IDbConnection connection, ISqlGenerator<MailType> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        public IEnumerable<MailType> GetPaged(int sinceId, int count, long UserID)
        {
            IEnumerable<MailType> mailtypes = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                mailtypes = cn.Query<MailType>("SELECT TOP " + count + " * FROM MailType WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return mailtypes;
        }

        public async Task<IEnumerable<MailType>> GetPagedAsync(int sinceId, int count, long UserID)
        {
            IEnumerable<MailType> mailtypes = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();

                var dbArgs = new DynamicParameters();
                dbArgs.Add("sinceId", sinceId);
                dbArgs.Add("UserID", UserID);

                mailtypes = await cn.QueryAsync<MailType>("SELECT TOP " + count + " * FROM MailType WHERE ID > @sinceId AND UserID = @UserID", dbArgs);
            }

            return mailtypes;
        }
    }
}