#nullable disable
namespace BookStore1.Models
{
    public class OrderDetail
    {
        public int Id { get; set; } // Primary key
        public int OrderId { get; set; } // Foreign key to Order
        public int BookId { get; set; } // Foreign key to Book
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}