using Salil_MVC.DataAccess.Data;
using Salil_MVC.DataAccess.Repository.IRepository;
using Salil_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salil_MVC.DataAccess.Repository
{
    public class ProductRespository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRespository(ApplicationDbContext applicationDBContext):base(applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }
        //public void Save()
        //{
        //    _dbContext.SaveChanges();
        //}

        public void Update(Product product)
        {
            var objFromDb = _dbContext.Products.FirstOrDefault(u => u.Id == product.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price100 = product.Price100;
                objFromDb.Description = product.Description;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.Author = product.Author;
                objFromDb.ImageUrl = product.ImageUrl;
            }
        }
    }
}
