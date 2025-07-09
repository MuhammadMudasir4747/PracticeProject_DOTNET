using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace PracticeProject.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T will be category or any other generic model on
        // which we want to perform operations or rather we want
        //to interact with DbContext
        
        IEnumerable<T> GetAll(string includeProperties); //retreiving all catgs
        T Get(Expression<Func<T, bool>> filter); //Get individual
        void Add(T entity);
     
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


    }
}
