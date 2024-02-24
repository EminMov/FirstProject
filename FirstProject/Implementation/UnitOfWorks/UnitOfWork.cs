using FirstProject.Abstractions.IRepositories;
using FirstProject.Abstractions.IUnitOfWorks;
using FirstProject.Contexts;
using FirstProject.Entities;
using FirstProject.Implementation.Repositories;

namespace FirstProject.Implementation.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dbContext;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(ApplicationContext dbContext, Dictionary<Type, object> repositories) 
        { 
            _dbContext = dbContext;
            _repositories = repositories;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            IRepository<TEntity> repository = new Repository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();

        }
    }
}
