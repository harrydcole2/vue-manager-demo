using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Context;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;
using Dapper;

namespace ClientDashboardAPI.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientDashboardContext _context;

        public ClientRepository(ClientDashboardContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Client client)
        {
            using var connection = _context.GetConnection();

            await connection.ExecuteAsync(
                "INSERT INTO Clients (Name, Description, UserId) "
                    + "VALUES (@Name, @Description, @UserId",
                new
                {
                    client.Name,
                    client.Description,
                    client.UserId,
                }
            );
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _context.GetConnection();

            await connection.ExecuteAsync("DELETE FROM Clients WHERE Id = @Id", new { Id = id });
        }

        public async Task<List<Client>> GetAllAsync()
        {
            using var connection = _context.GetConnection();

            var clients = await connection.QueryAsync<Client>("SELECT * FROM Clients");
            return clients.AsList();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            using var connection = _context.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<Client>(
                "SELECT * FROM Clients WHERE Id = @Id",
                new { Id = id }
            );
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            using var connection = _context.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<Client>(
                "UPDATE Clients SET Name = @Name, Description = @Description WHERE Id = @Id",
                new { client.Name, client.Description }
            );
        }

        public async Task ArchiveAsync(int id)
        {
            using var connection = _context.GetConnection();

            await connection.ExecuteAsync(
                "UPDATE Clients SET Archived = TRUE WHERE Id = @Id",
                new { Id = id }
            );
        }
    }
}