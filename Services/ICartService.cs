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
}
