using System;

namespace ClientDashboardAPI.Data.Model
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int ClientId { get; set; } // FK
        public string Phone { get; set; } = string.Empty;
    }
}