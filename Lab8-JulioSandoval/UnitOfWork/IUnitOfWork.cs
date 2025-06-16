using Lab8_JulioSandoval.Repositories;

namespace Lab8_JulioSandoval.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();
    }
}