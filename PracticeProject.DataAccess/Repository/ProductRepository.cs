using PracticeProject.DataAccess.Repository.IRepository;
using PracticeProject.Models;
using PracticeProject_DOTNET.DataAccess.Data;
using System;
using System.Linq;
using System.Linq.Expressions;



namespace PracticeProject.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private MyNewDbContext _db;

        public ProductRepository(MyNewDbContext db) : base(db)
        {
            _db = db;
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            return dbSet.Where(filter).FirstOrDefault();
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
