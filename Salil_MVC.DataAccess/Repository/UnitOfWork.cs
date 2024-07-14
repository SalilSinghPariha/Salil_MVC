using Salil_MVC.DataAccess.Data;
using Salil_MVC.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salil_MVC.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWorks
    {
        public ICategoryRepository CategoryRepository { get;private set; }

        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext applicationDBContext)
        {
            _dbContext = applicationDBContext;

            CategoryRepository = new CategoryRespository(_dbContext);
        }
        public void Dispose()
        {
           _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
