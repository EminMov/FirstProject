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
        public UnitOfWork(ApplicationContext dbContext) 
        { 
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity))) //key
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)]; //value
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
