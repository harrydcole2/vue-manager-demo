using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClientDashboardAPI.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);

        Task<User> GetUserByEmail(string email);

        Task<int> CreateUser(User user);
    }
}