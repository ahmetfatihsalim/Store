using Microsoft.EntityFrameworkCore;
using SaleStore.Data;
using SaleStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class // giving our implementation class a generic class. because our interface also takes a generic class
    {
        public readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet; // internal is for assembly scope
        public Repository(ApplicationDbContext db)
        {
            _db = db; //Example Dependency Injection
            this.dbSet = _db.Set<T>(); // so we can implement crud and other operations on our generic classes
            //_db.Categories = dbSet
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet; // get the dbset
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet; // get the dbset
            query = dbSet.Where(filter); // give our where clause
            return query.FirstOrDefault(); // get the first result for thats what we want
        }
    }
}
