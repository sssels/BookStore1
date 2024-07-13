using BookStore1.Models;
using BookStore1.Data;
using Microsoft.EntityFrameworkCore;
#nullable disable
public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddToCart(CartItem cartItem)
    {
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CartItem>> GetCartItems(int userId)
    {
        return await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();
    }
}

