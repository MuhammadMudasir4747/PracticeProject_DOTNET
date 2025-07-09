using PracticeProject.Models;
using System;
using System.Linq.Expressions;

namespace PracticeProject.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        Product Get(Expression<Func<Product, bool>> filter);
    }
}
