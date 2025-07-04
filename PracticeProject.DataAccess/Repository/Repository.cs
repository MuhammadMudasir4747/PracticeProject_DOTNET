﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticeProject.DataAccess.Repository.IRepository;
using PracticeProject_DOTNET.DataAccess.Data;

namespace PracticeProject.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly MyNewDbContext _db;
        
        internal DbSet<T> dbSet;

        public Repository(MyNewDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //_db.Categories == dbSet
        }
        public void Add(T entity)
        {
           dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query= query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();

        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);      
        }

      
    }
}
