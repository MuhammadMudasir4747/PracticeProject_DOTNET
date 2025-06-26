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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private MyNewDbContext _db;
        public CategoryRepository(MyNewDbContext db) : base (db) 
        {
            _db = db;   
        }
      

        public void Update(Category obj)
        {
           _db.Categories.Update(obj);
        }

    
    }
}
