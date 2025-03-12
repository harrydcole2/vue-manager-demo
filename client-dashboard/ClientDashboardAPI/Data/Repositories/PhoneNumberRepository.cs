using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Context;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;
using Dapper;

namespace ClientDashboardAPI.Data.Repositories
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly ClientDashboardContext _context;

        public PhoneNumberRepository(ClientDashboardContext context)
        {
            _context = context;
        }

        public async Task<List<PhoneNumber>> GetPhoneNumbersByClientId(int clientId)
        {
            using var connection = _context.GetConnection();

            var query =
                @"
                SELECT Id, ClientId, PhoneNumber
                FROM PhoneNumbers
                WHERE ClientId = @ClientId";

            var phoneNumbers = await connection.QueryAsync<PhoneNumber>(
                query,
                new { ClientId = clientId }
            );
            return phoneNumbers.AsList();
        }

        public async Task<int> AddPhoneNumber(PhoneNumber phoneNumber)
        {
            using var connection = _context.GetConnection();

            var query =
                @"
                        INSERT INTO PhoneNumbers (ClientId, PhoneNumber)
                        VALUES (@ClientId, @PhoneNumber);
                        SELECT LAST_INSERT_ID()";

            return await connection.ExecuteScalarAsync<int>(query, phoneNumber);
        }

        public async Task<bool> DeletePhoneNumber(int id)
        {
            using var connection = _context.GetConnection();

            var query = "DELETE FROM PhoneNumbers WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}