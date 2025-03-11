using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;

namespace ClientDashboardAPI.Data.Repositories
{
    public class PhoneNumberRepository : ICrudRepository<PhoneNumber>
    {
        public Task<PhoneNumber> CreateAsync(PhoneNumber entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<PhoneNumber>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PhoneNumber> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PhoneNumber> UpdateAsync(PhoneNumber entity)
        {
            throw new System.NotImplementedException();
        }
    }
}