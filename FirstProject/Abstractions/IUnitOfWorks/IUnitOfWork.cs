using FirstProject.Abstractions.IRepositories;
using FirstProject.Entities;

namespace FirstProject.Abstractions.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
