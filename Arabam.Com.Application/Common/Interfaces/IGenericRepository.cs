namespace Arabam.Com.Application.Common.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(T entity);
    }
}
