using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork // general interface for the general functions we'll use in other interfaces
    {
        ICategoryRepository CategoryRepository { get; } // we dont set anything
        IProductRepository ProductRepository { get; }

        void Save();
    }
}
