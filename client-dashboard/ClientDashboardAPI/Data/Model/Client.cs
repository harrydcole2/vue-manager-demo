using System;

namespace ClientDashboardAPI.Data.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public int UserId { get; set; } // FK
    }
}