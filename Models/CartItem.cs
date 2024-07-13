using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BookStore1.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        // Optional: Reference to the Book
        public Book Book { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
