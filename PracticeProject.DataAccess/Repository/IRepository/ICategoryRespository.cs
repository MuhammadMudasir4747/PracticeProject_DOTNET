using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.DataAccess.Repository.IRepository
{
    public interface ICategoryRespository : IRepository<Category>
    {
        void Update(Category obj);
        void Save();
    }
}
