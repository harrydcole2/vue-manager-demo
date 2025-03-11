using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;

namespace ClientDashboardAPI.Data.Repositories
{
    public class ProductRepository : ICrudRepository<Product>
    {
        public Task CreateAsync(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Product>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            throw new System.NotImplementedException();
        }
    }
}