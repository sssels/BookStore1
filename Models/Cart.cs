using System.Collections.Generic;
#nullable disable
namespace BookStore1.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
         public List<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; } // This should match the type of Cart's primary key
        public string Title { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Cart Cart { get; set; }
        public Book Book { get; set; }
    }
}
