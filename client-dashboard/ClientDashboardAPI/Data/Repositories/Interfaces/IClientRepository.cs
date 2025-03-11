using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;

namespace ClientDashboardAPI.Data.Repositories.Interfaces
{
    public interface IClientRepository : ICrudRepository<Client>
    {
        public Task ArchiveAsync(int id);
    }
}