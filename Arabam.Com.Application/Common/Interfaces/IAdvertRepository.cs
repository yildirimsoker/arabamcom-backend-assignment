using Arabam.Com.Domain.Common;

namespace Arabam.Com.Application.Common.Interfaces
{
    public interface IAdvertRepository : IGenericRepository<Arabam.Com.Domain.Entities.Adverts>
    {
        Task<IEnumerable<Arabam.Com.Domain.Entities.Adverts>> GetAllAsync(Filter filter);
        Task<int> GetCountAsync(Filter filter);
        Task<bool> GetAnyAsync(int id);
    }
}
