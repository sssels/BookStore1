using System.Collections.Generic;
using System.Linq;
using BookStore1.Models;

namespace BookStore1.Services
{
    public class CartService : ICartService
    {
        private readonly Dictionary<string, Cart> _userCarts = new Dictionary<string, Cart>();

        public Cart GetCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");
            }

            if (!_userCarts.ContainsKey(userId))
            {
                _userCarts[userId] = new Cart { UserId = userId };
            }
            return _userCarts[userId];
        }

        public void AddToCart(string userId, int bookId, string title, decimal price, int quantity)
        {
            var cart = GetCart(userId);
            var existingItem = cart.Items.FirstOrDefault(item => item.BookId == bookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    BookId = bookId,
                    Title = title,
                    Price = price,
                    Quantity = quantity
                });
            }
        }

        public void RemoveFromCart(string userId, int bookId)
        {
            var cart = GetCart(userId);
            var itemToRemove = cart.Items.FirstOrDefault(item => item.BookId == bookId);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
            }
        }

        public void ClearCart(string userId)
        {
            var cart = GetCart(userId);
            cart.Items.Clear();
        }

        public void Checkout(string userId)
        {
            var cart = GetCart(userId);
            // Add order creation and stock deduction logic here.
            ClearCart(userId); // Clear the cart
        }
    }
}
