using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BookStore1.Data;
using BookStore1.Models;

namespace BookStore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // HTTP POST api/carts/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartModel model)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Title == model.Title);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == model.UserId);

            if (cart == null)
            {
                cart = new Cart { UserId = model.UserId };
                _context.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == book.Id);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    BookId = book.Id,
                    Quantity = model.Quantity,
                    Cart = cart
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += model.Quantity;
            }

            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        // HTTP POST api/carts/remove
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartModel model)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Title == model.Title);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == model.UserId);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == book.Id);
            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            cart.CartItems.Remove(cartItem);
            if (!cart.CartItems.Any())
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}