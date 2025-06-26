using PracticeProject.DataAccess.Repository.IRepository;
using PracticeProject.Models;
using PracticeProject_DOTNET.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private MyNewDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product {  get; private set; }

        

        public UnitOfWork(MyNewDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);

            Product = new ProductRepository(_db);   

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
