using SaleStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category> // now we give IRepository it's class
    {
        void Update(Category category);
    }
}
