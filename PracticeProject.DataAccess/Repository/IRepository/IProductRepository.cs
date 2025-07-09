using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Get(Func<Product, bool> value);
        void Update(Product obj);
       
       
    }
}
