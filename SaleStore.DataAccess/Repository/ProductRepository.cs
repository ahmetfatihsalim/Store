using SaleStore.Data;
using SaleStore.DataAccess.Repository.IRepository;
using SaleStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productFromDB = _db.Products.FirstOrDefault(dbProduct => dbProduct.ID == product.ID);
            if (productFromDB != null)  // manual mapping
            {
                productFromDB.Title= product.Title;
                productFromDB.Description= product.Description;
                productFromDB.ISBN= product.ISBN;
                productFromDB.Author= product.Author;
                productFromDB.ListPrice= product.ListPrice;
                productFromDB.Price= product.Price;
                productFromDB.Price50= product.Price50;
                productFromDB.Price100= product.Price100;
                productFromDB.CategoryID= product.CategoryID;
                if (productFromDB.ImageUrl != null)
                {
                    productFromDB.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}