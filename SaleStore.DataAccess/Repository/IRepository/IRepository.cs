using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class // Take a T object. This object will be class
    {
        // Generic interface for our classes and interaction with DB
        IEnumerable<T> GetAll(string? includeProperties = null);
        T GetFirstOrDefault(Expression<Func<T,bool>> filter, string? includeProperties = null); // we'll use a linq expression which is a function that takes T and returns boolean result
        void Add(T entity);

        //void Update(T entity); // Update logic may vary so we dont add update function here
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities); // delete multiple entities. Get these entities as IEnumerable

        //void Save(); // In comment for the same reason as update
    }
}
