using System;

namespace ClientDashboardAPI.Data.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; } // FK
    }
}