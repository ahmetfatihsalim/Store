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

        /// <summary>
        /// Given parameter is for navigation. 
        /// It will get related object keys which are seperated by whatever we define for seperation
        /// Object key name must exactly match the name of property of the given T class
        /// Related object is to be populated based on foreign key relation. 
        /// Ex : _db.Products.Include(p => p.Category).Include(x => x.SomethingElse). ...;
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet; // get the dbset
            query = IncludePropertiesForNavigation(query, includeProperties);
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet; // get the dbset
            query = dbSet.Where(filter); // give our where clause
            query = IncludePropertiesForNavigation(query, includeProperties);
            return query.FirstOrDefault(); // get the first result for thats what we want
        }

        private IQueryable<T> IncludePropertiesForNavigation(IQueryable<T> query, string? includeProperties = null) // I think this can be customized in different ways. I'm not gonna put this in interface
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }
    }
}
