using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;

namespace ClientDashboardAPI.Data.Repositories
{
    public class OrderRepository : ICrudRepository<Order>
    {
        public Task CreateAsync(Order entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Order>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> UpdateAsync(Order entity)
        {
            throw new System.NotImplementedException();
        }
    }
}