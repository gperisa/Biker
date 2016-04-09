using BikeGround.DataLayer.Repositories.Base;
using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    /// <summary>
    /// Repozitorij za pretraživanje drugih korisnika
    /// </summary>
    public class ConnectRepository : DataConnection
    {
        private string SQL = "SELECT {0} FROM [dbo].[Profile] LEFT OUTER JOIN [dbo].[Blog] ON [dbo].[Profile].[UserID] = [dbo].[Blog].[UserID] WHERE {1} AND [dbo].[Profile].[UserID] <> @UserID";

        public ConnectRepository(IDbConnection connection)
            : base(connection)
        {
        }

        public async Task<IEnumerable<Connect>> GetPaged(Connect obj, long UserID)
        {
            DynamicQueryHelper param = obj.GetDynamicQueryData();

            param.dbArgs.Add("UserID", UserID);

            string query = String.Format(this.SQL,
                param.getAtributes,
                param.getParametars);

            return await Connection.QueryAsync<Connect>(query, param.dbArgs);
        }
    }
}