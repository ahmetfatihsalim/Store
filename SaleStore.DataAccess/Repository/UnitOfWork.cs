using SaleStore.Data;
using SaleStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db; // same as category repository
        public ICategoryRepository CategoryRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db) // we dont have any base class
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
        }

        public void Save()
        {
            // normally there needs to be more functionalities here for this class to actually be unit of work. Like cancelling token or listening something before saving 
            _db.SaveChanges();
        }
    }
}
