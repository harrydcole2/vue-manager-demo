using System.Threading.Tasks;
using ClientDashboardAPI.Context;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace ClientDashboardAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ClientDashboardContext _context;

        public UserRepository(ClientDashboardContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int id)
        {
            using var connection = _context.GetConnection();

            var query = "SELECT Id, Email, PasswordHash FROM Users WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }

        public async Task<User> GetUserByEmail(string email)
        {
            using var connection = _context.GetConnection();

            var query = "SELECT Id, Email, PasswordHash FROM Users WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task<int> CreateUser(User user)
        {
            using var connection = _context.GetConnection();

            var query =
                @"
                INSERT INTO Users (Email, PasswordHash)
                VALUES (@Email, @PasswordHash);
                SELECT LAST_INSERT_ID();";

            return await connection.ExecuteScalarAsync<int>(query, user);
        }
    }
}