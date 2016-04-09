using Dapper;
using Dapper.DataRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeGround.DataLayer.Repositories
{
    /// <summary>
    /// Repozitorij za dohvaćanje pojedinačne vrijednosti iz baze
    /// </summary>
    public class SinglePropertyRepository : DataConnection
    {
        public SinglePropertyRepository(IDbConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Dohvati property iz baze za zadani objekt tipa T
        /// </summary>
        /// <typeparam name="T">POCO klasa koja reprezentira tablicu</typeparam>
        /// <param name="property">Property koji se želi odabrati</param>
        /// <param name="UserID">Korisnik ID</param>
        /// <returns>string -> property</returns>
        public string GetSingleProperty<T>(Expression<Func<T, string>> property, long UserID) where T : class
        {
            Type t = typeof(T);

            string _property;

            if (property.Body is MemberExpression)
            {
                _property = ((MemberExpression)property.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)property.Body).Operand;
                _property = ((MemberExpression)op).Member.Name;
            }

            var dbArgs = new DynamicParameters();
            dbArgs.Add("UserID", UserID);
            string sql = String.Format("SELECT TOP 1 {1} FROM {0} WHERE UserID = @UserID", t.Name, _property);
            IEnumerable<string> l = Connection.Query<string>(sql, dbArgs);

            if (l == null || l.Count() == 0)
            {
                return "";
            }

            return l.Single();
        }

        /// <summary>
        /// Dohvati property iz baze za zadani objekt tipa T
        /// </summary>
        /// <typeparam name="T">POCO klasa koja reprezentira tablicu</typeparam>
        /// <param name="property">Property koji se želi odabrati</param>
        /// <param name="UserID">Korisnik ID</param>
        /// <returns>bool -> property</returns>
        public bool GetSingleProperty<T>(Expression<Func<T, bool>> property, long UserID) where T : class
        {
            Type t = typeof(T);

            string _property;

            if (property.Body is MemberExpression)
            {
                _property = ((MemberExpression)property.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)property.Body).Operand;
                _property = ((MemberExpression)op).Member.Name;
            }

            var dbArgs = new DynamicParameters();
            dbArgs.Add("UserID", UserID);
            string sql = String.Format("SELECT TOP 1 {1} FROM {0} WHERE UserID = @UserID", t.Name, _property);
            IEnumerable<bool> l = Connection.Query<bool>(sql, dbArgs);

            if (l == null || l.Count() == 0)
            {
                return false;
            }

            return l.Single();
        }

        /// <summary>
        /// Dohvati property iz baze za zadani objekt tipa T
        /// </summary>
        /// <typeparam name="T">POCO klasa koja reprezentira tablicu</typeparam>
        /// <param name="property">Property koji se želi odabrati</param>
        /// <param name="UserID">Korisnik ID</param>
        /// <returns>long -> property</returns>
        public long GetSingleProperty<T>(Expression<Func<T, long>> property, long UserID) where T : class
        {
            Type t = typeof(T);

            string _property;

            if (property.Body is MemberExpression)
            {
                _property = ((MemberExpression)property.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)property.Body).Operand;
                _property = ((MemberExpression)op).Member.Name;
            }

            var dbArgs = new DynamicParameters();
            dbArgs.Add("UserID", UserID);
            string sql = String.Format("SELECT TOP 1 {1} FROM {0} WHERE UserID = @UserID", t.Name, _property);
            IEnumerable<long> l = Connection.Query<long>(sql, dbArgs);

            if (l == null || l.Count() == 0)
            {
                return 0;
            }

            return l.Single();
        }

        /// <summary>
        /// Dohvati property iz baze za zadani objekt tipa T, async
        /// </summary>
        /// <typeparam name="T">POCO klasa koja reprezentira tablicu</typeparam>
        /// <param name="property">Property koji se želi odabrati</param>
        /// <param name="UserID">Korisnik ID</param>
        /// <returns>bool -> property</returns>
        public async Task<bool> GetSinglePropertyAsync<T>(Expression<Func<T, bool>> property, long UserID) where T : class
        {
            Type t = typeof(T);

            string _property;

            if (property.Body is MemberExpression)
            {
                _property = ((MemberExpression)property.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)property.Body).Operand;
                _property = ((MemberExpression)op).Member.Name;
            }

            var dbArgs = new DynamicParameters();
            dbArgs.Add("UserID", UserID);
            string sql = String.Format("SELECT TOP 1 {1} FROM {0} WHERE UserID = @UserID", t.Name, _property);
            IEnumerable<bool> l = await Connection.QueryAsync<bool>(sql, dbArgs);

            if (l == null || l.Count() == 0)
            {
                return false;
            }

            return l.Single();
        }

        /// <summary>
        /// Dohvati property iz baze za zadani objekt tipa T, async
        /// </summary>
        /// <typeparam name="T">POCO klasa koja reprezentira tablicu</typeparam>
        /// <param name="property">Property koji se želi odabrati</param>
        /// <param name="UserID">Korisnik ID</param>
        /// <returns>string -> property</returns>
        public async Task<string> GetSinglePropertyAsync<T>(Expression<Func<T, string>> property, long UserID) where T : class
        {
            Type t = typeof(T);

            string _property;

            if (property.Body is MemberExpression)
            {
                _property = ((MemberExpression)property.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)property.Body).Operand;
                _property = ((MemberExpression)op).Member.Name;
            }

            var dbArgs = new DynamicParameters();
            dbArgs.Add("UserID", UserID);
            string sql = String.Format("SELECT TOP 1 {1} FROM {0} WHERE UserID = @UserID", t.Name, _property);
            IEnumerable<string> l = await Connection.QueryAsync<string>(sql, dbArgs);

            if (l == null || l.Count() == 0)
            {
                return "";
            }

            return l.Single();
        }

        /// <summary>
        /// Dohvati property iz baze za zadani objekt tipa T, async
        /// </summary>
        /// <typeparam name="T">POCO klasa koja reprezentira tablicu</typeparam>
        /// <param name="property">Property koji se želi odabrati</param>
        /// <param name="UserID">Korisnik ID</param>
        /// <returns>long -> property</returns>
        public async Task<long> GetSinglePropertyAsync<T>(Expression<Func<T, long>> property, long UserID) where T : class
        {
            Type t = typeof(T);

            string _property;

            if (property.Body is MemberExpression)
            {
                _property = ((MemberExpression)property.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)property.Body).Operand;
                _property = ((MemberExpression)op).Member.Name;
            }

            var dbArgs = new DynamicParameters();
            dbArgs.Add("UserID", UserID);
            string sql = String.Format("SELECT TOP 1 {1} FROM {0} WHERE UserID = @UserID", t.Name, _property);
            IEnumerable<long> l = await Connection.QueryAsync<long>(sql, dbArgs);

            if (l == null || l.Count() == 0)
            {
                return 0;
            }

            return l.Single();
        }
    }
}