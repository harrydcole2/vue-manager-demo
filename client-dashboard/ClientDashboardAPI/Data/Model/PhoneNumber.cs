using System;

namespace ClientDashboardAPI.Data.Model
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int ClientId { get; set; } // FK
        public string Number { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}