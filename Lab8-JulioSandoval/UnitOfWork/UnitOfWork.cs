using Lab8_JulioSandoval.Models;
using Lab8_JulioSandoval.Repositories;
using System.Collections;

namespace Lab8_JulioSandoval.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LinqExampleContext _context;
        private Hashtable? _repositories;

        public UnitOfWork(LinqExampleContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;

            if (_repositories!.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type]!;
            }

            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            if (repositoryInstance != null)
            {
                _repositories.Add(type, repositoryInstance);
                return (IRepository<TEntity>)repositoryInstance;
            }

            throw new Exception($"Could not create repository instance for type {type}");
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}