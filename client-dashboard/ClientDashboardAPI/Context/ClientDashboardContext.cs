using System.Data;
using System.Data.SqlClient;
using ClientDashboardAPI.Data.Model;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ClientDashboardAPI.Context
{
    public class ClientDashboardContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ClientDashboardContext(IConfiguration configuration)
        {
            _configuration = configuration;

            string host = _configuration["DBHOST"] ?? "database";
            string user = _configuration["ConnectionStrings:MYSQL_USER"] ?? "newuser";
            string password = _configuration["ConnectionStrings:MYSQL_PASSWORD"] ?? "password";
            string database =
                _configuration["ConnectionStrings:MYSQL_DATABASE"] ?? "ClientDashboardDB";

            _connectionString =
                $"Server={host};Port=3306;Database={database};User={user};Password={password};";
        }

        public IDbConnection GetConnection() => new MySqlConnection(_connectionString);
    }
}
