using PracticeProject.DataAccess.Repository.IRepository;
using PracticeProject.Models;
using PracticeProject_DOTNET.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository

    {
        private MyNewDbContext _db;
        public ProductRepository(MyNewDbContext db) : base (db) 
        {
            _db = db;   
        }
      

        public void Update(Product obj)
        {
           _db.Products.Update(obj);
        }

    
    }
}
