﻿using SaleStore.Data;
using SaleStore.DataAccess.Repository.IRepository;
using SaleStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository // We also inherit Repository class. So we dont redefine our crud functions
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db) // whatever db context implementation we gave, passes to base class (which is Repository.cs)
        {
            _db= db;
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
