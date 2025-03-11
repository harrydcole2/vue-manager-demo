using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;

namespace ClientDashboardAPI.Data.Repositories
{
    public class UserRepository : ICrudRepository<User>
    {
        public Task<User> CreateAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}