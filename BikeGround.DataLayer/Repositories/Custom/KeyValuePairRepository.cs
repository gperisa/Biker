using BikeGround.Models.Helpers;
using Dapper;
using Dapper.DataRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    /// <summary>
    /// Sadrži metode pomoću kojih dohvaćamo key value pair iz zadanih eniteta baze
    /// </summary>
    public class KeyValuePairRepository : DataConnection
    {
        public KeyValuePairRepository(IDbConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Vraća key value pair definiran iz zadanog entiteta
        /// </summary>
        /// <typeparam name="T">Objekt unutar kojeg radimo query</typeparam>
        /// <param name="ID">Long vrijednsot za id (ID)</param>
        /// <param name="Description">Long vrijednost za opis (Title)</param>
        /// <returns>IEnumerable DDHelper objekt</returns>
        public IEnumerable<DDHelper> GetKeyValuePair<T>(Expression<Func<T, long>> ID, Expression<Func<T, string>> Description) where T : class
        {
            Type t = typeof(T);

            string _ID;
            string _Description;

            if (ID.Body is MemberExpression)
            {
                _ID = ((MemberExpression)ID.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)ID.Body).Operand;
                _ID = ((MemberExpression)op).Member.Name;
            }

            if (Description.Body is MemberExpression)
            {
                _Description = ((MemberExpression)Description.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)Description.Body).Operand;
                _Description = ((MemberExpression)op).Member.Name;
            }

            IEnumerable<DDHelper> pairs = null;

            string sql = String.Format("SELECT {1} as 'ID', {2} as 'Title' FROM {0}", t.Name, _ID, _Description);
            pairs = Connection.Query<DDHelper>(sql);

            return pairs;
        }

        /// <summary>
        /// Vraća key value pair definiran iz zadanog entiteta
        /// </summary>
        /// <typeparam name="T">Objekt unutar kojeg radimo query</typeparam>
        /// <param name="ID">Long vrijednsot za id (ID)</param>
        /// <param name="Description">Long vrijednost za opis (Title)</param>
        /// <returns>IEnumerable DDHelper objekt</returns>
        public async Task<IEnumerable<DDHelper>> GetKeyValuePairAsync<T>(Expression<Func<T, long>> ID, Expression<Func<T, string>> Description) where T : class
        {
            Type t = typeof(T);

            string _ID;
            string _Description;

            if (ID.Body is MemberExpression)
            {
                _ID = ((MemberExpression)ID.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)ID.Body).Operand;
                _ID = ((MemberExpression)op).Member.Name;
            }

            if (Description.Body is MemberExpression)
            {
                _Description = ((MemberExpression)Description.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)Description.Body).Operand;
                _Description = ((MemberExpression)op).Member.Name;
            }

            IEnumerable<DDHelper> pairs = null;

            string sql = String.Format("SELECT {1} as 'ID', {2} as 'Title' FROM {0}", t.Name, _ID, _Description);
            pairs = await Connection.QueryAsync<DDHelper>(sql);

            return pairs;
        }

        /// <summary>
        /// Vraća key value pair definiran iz zadanog entiteta filtriran za pojedinog korisnika (UserID)
        /// </summary>
        /// <typeparam name="T">Objekt unutar kojeg radimo query</typeparam>
        /// <param name="ID">Long vrijednsot za id (ID)</param>
        /// <param name="Description">Long vrijednost za opis (Title)</param>
        /// <param name="UserID">Ciljani user (UserID)</param>
        /// <returns>IEnumerable DDHelper objekt</returns>
        public IEnumerable<DDHelper> GetKeyValuePair<T>(Expression<Func<T, long>> ID, Expression<Func<T, string>> Description, long UserID) where T : class
        {
            Type t = typeof(T);

            string _ID;
            string _Description;

            if (ID.Body is MemberExpression)
            {
                _ID = ((MemberExpression)ID.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)ID.Body).Operand;
                _ID = ((MemberExpression)op).Member.Name;
            }

            if (Description.Body is MemberExpression)
            {
                _Description = ((MemberExpression)Description.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)Description.Body).Operand;
                _Description = ((MemberExpression)op).Member.Name;
            }

            IEnumerable<DDHelper> pairs = null;

            var dbArgs = new DynamicParameters();
            dbArgs.Add("UserID", UserID);
            string sql = String.Format("SELECT {1} as 'ID', {2} as 'Title' FROM {0} WHERE UserID > @UserID", t.Name, _ID, _Description);
            pairs = Connection.Query<DDHelper>(sql, dbArgs);

            return pairs;
        }

        /// <summary>
        /// Vraća key value pair definiran iz zadanog entiteta filtriran za pojedinog korisnika (UserID)
        /// </summary>
        /// <typeparam name="T">Objekt unutar kojeg radimo query</typeparam>
        /// <param name="ID">Long vrijednsot za id (ID)</param>
        /// <param name="Description">Long vrijednost za opis (Title)</param>
        /// <param name="UserID">Ciljani user (UserID)</param>
        /// <returns>IEnumerable DDHelper objekt</returns>
        public async Task<IEnumerable<DDHelper>> GetKeyValuePairAsync<T>(Expression<Func<T, long>> ID, Expression<Func<T, string>> Description, long UserID) where T : class
        {
            Type t = typeof(T);

            string _ID;
            string _Description;

            if (ID.Body is MemberExpression)
            {
                _ID = ((MemberExpression)ID.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)ID.Body).Operand;
                _ID = ((MemberExpression)op).Member.Name;
            }

            if (Description.Body is MemberExpression)
            {
                _Description = ((MemberExpression)Description.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)Description.Body).Operand;
                _Description = ((MemberExpression)op).Member.Name;
            }

            IEnumerable<DDHelper> pairs = null;

            var dbArgs = new DynamicParameters();
            dbArgs.Add("userId", UserID);

            string sql = String.Format("SELECT {1} as 'ID', {2} as 'Title' FROM {0} WHERE UserID = @userId", t.Name, _ID, _Description);
            pairs = await Connection.QueryAsync<DDHelper>(sql, dbArgs);

            return pairs;
        }
    }
}