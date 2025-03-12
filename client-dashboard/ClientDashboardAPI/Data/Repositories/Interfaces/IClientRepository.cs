using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;

namespace ClientDashboardAPI.Data.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetClientsByUserId(int userId, bool archived);

        Task<Client> GetClientById(int id, int userId);

        Task<int> CreateClient(Client client);

        Task<bool> UpdateClient(Client client);
    }
}