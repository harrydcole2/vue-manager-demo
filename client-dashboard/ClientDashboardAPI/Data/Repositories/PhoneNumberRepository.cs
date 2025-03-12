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
            var connection = _context.GetConnection();

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
            var connection = _context.GetConnection();

            var query =
                @"
                INSERT INTO PhoneNumbers (ClientId, PhoneNumber)
                VALUES (@ClientId, @PhoneNumber);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            return await connection.QuerySingleAsync<int>(query, phoneNumber);
        }

        public async Task<bool> DeletePhoneNumber(int id)
        {
            var connection = _context.GetConnection();

            var query = "DELETE FROM PhoneNumbers WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}