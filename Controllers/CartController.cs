using Microsoft.AspNetCore.Mvc;
using BookStore1.Data;
using BookStore1.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookStore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.Name; // Kullanıcı kimliği alınıyor, auth işlemi sonrası doldurulmalı

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new BookStore1.Models.Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == input.BookId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    BookId = input.BookId,
                    Title = input.Title,
                    Price = input.Price,
                    Quantity = input.Quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += input.Quantity;
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.Name; // Kullanıcı kimliği alınıyor, auth işlemi sonrası doldurulmalı

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == input.BookId);
            if (cartItem == null)
            {
                return NotFound("Item not found in cart.");
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return Ok(cartItem);
        }
    }
}
