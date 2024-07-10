using BookStore1.Models;
public interface ICartService
{
    Task AddToCart(CartItem cartItem);
    Task<IEnumerable<CartItem>> GetCartItems(int userId);
}
