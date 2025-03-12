using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;

namespace ClientDashboardAPI.Data.Repositories.Interfaces
{
    public interface IPhoneNumberRepository
    {
        Task<List<PhoneNumber>> GetPhoneNumbersByClientId(int clientId);

        Task<int> AddPhoneNumber(PhoneNumber phoneNumber);

        Task<bool> DeletePhoneNumber(int id);
    }
}