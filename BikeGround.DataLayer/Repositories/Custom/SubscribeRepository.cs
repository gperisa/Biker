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
    /// Repozitorij za dohvaćanje prijatelja
    /// </summary>
    public class SubscribeRepository : DataConnection
    {
        private string SQL = "SELECT {0} FROM [dbo].[Friend] INNER JOIN [dbo].[Profile] ON [dbo].[Friend].[FriendID] = [dbo].[Profile].[UserID] INNER JOIN [dbo].[AspNetUsers] ON [dbo].[Friend].[FriendID] = [dbo].[AspNetUsers].[Id] WHERE [dbo].[Friend].[UserID] = @UserID AND [dbo].[Friend].[ProfileActivityID] = 1";

        public SubscribeRepository(IDbConnection connection)
            : base(connection)
        {
        }

        public async Task<IEnumerable<Subscribe>> GetPaged(Subscribe obj, long UserID)
        {
            DynamicQueryHelper param = obj.GetDynamicQueryData();

            param.dbArgs.Add("UserID", UserID);

            string query = String.Format(this.SQL,
                param.getAtributes);

            return await Connection.QueryAsync<Subscribe>(query, param.dbArgs);
        }
    }
}