using BookStore1.Models;
#nullable disable
public interface ICartService
{
    Task AddToCart(CartItem cartItem);
    Task<IEnumerable<CartItem>> GetCartItems(int userId);
}
