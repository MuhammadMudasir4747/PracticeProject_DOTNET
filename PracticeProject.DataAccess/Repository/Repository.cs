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

     


        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();

        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);      
        }

        public IEnumerable<T> GetAll(string includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp); // EF Core Include
                }
            }

            return query.ToList();
        }

        public T Get(Expression<Func<T, bool>> filter, string includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }


    }
}
