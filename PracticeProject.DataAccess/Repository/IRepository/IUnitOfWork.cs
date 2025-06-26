using PracticeProject.DataAccess.Repository.IRepository;

namespace PracticeProject.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        void Save();
    }
}
