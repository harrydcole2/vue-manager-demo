using System;

namespace ClientDashboardAPI.Data.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; } // FK
        public int ProductId { get; set; } // FK
        public int UserId { get; set; } // FK
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}