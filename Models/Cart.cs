using System.Collections.Generic;
using System.Linq;

namespace BookStore1.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal TotalPrice => CartItems.Sum(item => item.Price * item.Quantity);
    }
}
