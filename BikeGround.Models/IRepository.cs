using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeGround.Models
{
    public interface IRepository<T> where T : class
    {
        IDbConnection Connection { get; }

        T Insert(T entity);
        T Update(T entity);
        bool Delete(int id);
        
        T FindById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetPaged(int sinceId, int count);
    }
}
