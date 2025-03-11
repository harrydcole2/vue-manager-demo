using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;

namespace ClientDashboardAPI.Data.Repositories.Interfaces
{
    public interface ICrudRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }
}