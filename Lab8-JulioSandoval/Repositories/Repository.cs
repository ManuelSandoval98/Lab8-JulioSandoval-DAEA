using Lab8_JulioSandoval.Models;
using Microsoft.EntityFrameworkCore;


namespace Lab8_JulioSandoval.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LinqExampleContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(LinqExampleContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}