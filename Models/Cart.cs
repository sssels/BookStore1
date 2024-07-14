using System.Collections.Generic;
#nullable disable
namespace BookStore1.Models
{
    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
