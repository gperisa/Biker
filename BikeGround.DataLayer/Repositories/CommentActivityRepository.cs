﻿using BikeGround.Models;
using Dapper;
using Dapper.DataRepositories;
using MicroOrm.Pocos.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    interface ICommentActivityRepository : IDataRepository<CommentActivity>
    {
        //ICommentActivityRepository is inheriting all CRUD operations 
    }

    public class CommentActivityRepository : DataRepository<CommentActivity>, ICommentActivityRepository
    {
        public CommentActivityRepository(IDbConnection connection, ISqlGenerator<CommentActivity> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}