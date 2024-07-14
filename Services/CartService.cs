using System.Collections.Generic;
using System.Linq;
using BookStore1.Models;
namespace BookStore1.Services
{
    public interface ICartService
    {
        Cart GetCart(string userId);
        void AddToCart(string userId, int bookId, string title, decimal price, int quantity);
        void RemoveFromCart(string userId, int bookId);
        void ClearCart(string userId);
        void Checkout(string userId);
    }

    public class CartService : ICartService
    {
        private readonly Dictionary<string, Cart> _userCarts = new Dictionary<string, Cart>();

        public Cart GetCart(string userId)
        {
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
            // Burada sipariş oluşturma veya stoktan düşme işlemleri yapılabilir.
            // Örneğin, sipariş veritabanına kaydedilebilir ve stoklardan düşme işlemi yapılabilir.
            // Bu adım sizin veritabanı modelinize ve iş mantığınıza göre değişebilir.
            ClearCart(userId); // Sepeti temizle
        }
    }
}
