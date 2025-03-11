﻿using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ClientDashboardAPI.Context
{
    public class ClientDashboardContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ClientDashboardContext(IConfiguration configuration)
        {
            _configuration = configuration;

            string user = configuration["ConnectionStrings:MYSQL_USER"];
            string password = configuration["ConnectionStrings:MYSQL_PASSWORD"];
            string database = configuration["ConnectionStrings:MYSQL_DATABASE"];

            _connectionString =
                $"Server=localhost;Database={database};User={user};Password={password};";
        }

        public IDbConnection GetConnection() => new SqlConnection(_connectionString);
    }
}