using System;
using System.Collections.Generic;
#nullable disable
namespace BookStore1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OrderDetails { get; set; }
        
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
