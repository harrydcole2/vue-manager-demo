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

        public async Task<List<Client>> GetClientsByUserId(int userId, bool archived)
        {
            using var connection = _context.GetConnection();

            var query =
                @"
                SELECT Id, Name, Description, IsArchived, UserId
                FROM Clients
                WHERE UserId = @UserId AND IsArchived = @IsArchived
                ORDER BY Name";

            var clients = await connection.QueryAsync<Client>(
                query,
                new { UserId = userId, IsArchived = archived }
            );
            return clients.AsList();
        }

        public async Task<Client> GetClientById(int id, int userId)
        {
            using var connection = _context.GetConnection();

            var query =
                @"
                SELECT Id, Name, Description, IsArchived, UserId
                FROM Clients
                WHERE Id = @Id AND UserId = @UserId";

            return await connection.QueryFirstOrDefaultAsync<Client>(
                query,
                new { Id = id, UserId = userId }
            );
        }

        public async Task<int> CreateClient(Client client)
        {
            using var connection = _context.GetConnection();

            var query =
                @"
                INSERT INTO Clients (Name, Description, IsArchived, UserId)
                VALUES (@Name, @Description, @IsArchived, @UserId);
                SELECT LAST_INSERT_ID()";

            return await connection.ExecuteScalarAsync<int>(query, client);
        }

        public async Task<bool> UpdateClient(Client client)
        {
            using var connection = _context.GetConnection();

            var query =
                @"
                UPDATE Clients
                SET Name = @Name,
                    Description = @Description,
                    IsArchived = @IsArchived
                WHERE Id = @Id AND UserId = @UserId";

            var rowsAffected = await connection.ExecuteAsync(query, client);
            return rowsAffected > 0;
        }
    }
}