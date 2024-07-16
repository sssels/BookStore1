using BookStore1.Data;
using BookStore1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

#nullable disable

namespace BookStore1.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            return View(cart);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int bookId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    BookId = bookId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        // POST: Cart/ClearCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // POST: Cart/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                // Burada ödeme işlemleri gerçekleştirilir
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
