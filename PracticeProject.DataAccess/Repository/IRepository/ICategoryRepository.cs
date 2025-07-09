using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category Get(Func<Category, bool> value);
        IEnumerable<Category> GetAll();
        void Update(Category obj);
       
       
    }
}
