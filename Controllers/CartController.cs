using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore1.Data;
using BookStore1.Models;
using BookStore1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // MVC Kısmı
        // GET: Cart/Index
        [HttpGet("/Cart/Index")]
        public IActionResult Index()
        {
            var userId = User.Identity.Name; // Kullanıcının kimliğini alın
            var cartItems = _context.CartItems
                .Include(c => c.Book)
                .Where(c => c.UserId == userId)
                .ToList();

            var cart = new Cart { CartItems = cartItems };

            return View(cart);
        }

        // GET: Cart/Checkout
        [HttpGet("/Cart/Checkout")]
        public IActionResult Checkout()
        {
            var userId = User.Identity.Name; // Kullanıcının kimliğini alın
            var cartItems = _context.CartItems
                .Include(c => c.Book)
                .Where(c => c.UserId == userId)
                .ToList();

            var cart = new Cart { CartItems = cartItems };

            return View(cart);
        }

        // POST: Cart/AddToCart
        [HttpPost("/Cart/AddToCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(CartViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.Name; // Kullanıcının kimliğini alın

                var cartItem = new CartItem
                {
                    BookId = model.BookId,
                    Quantity = model.Quantity,
                    UserId = userId
                };

                _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // POST: Cart/RemoveFromCart
        [HttpPost("/Cart/RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(CartViewModel model)
        {
            var userId = User.Identity.Name; // Kullanıcının kimliğini alın
            var cartItem = _context.CartItems.SingleOrDefault(c => c.BookId == model.BookId && c.UserId == userId);

            if (cartItem == null)
            {
                return NotFound(new { Message = "Item not found in cart" });
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // POST: Cart/CheckoutConfirmed
        [HttpPost("/Cart/CheckoutConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirmed()
        {
            var userId = User.Identity.Name; // Kullanıcının kimliğini alın
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();

            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index");
            }

            // Sipariş işleme kodunu buraya ekleyin

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // API Kısmı
        // POST: api/Cart/AddToCart
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCartApi([FromBody] CartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.Name; // Kullanıcının kimliğini alın

            var cartItem = new CartItem
            {
                BookId = model.BookId,
                Quantity = model.Quantity,
                UserId = userId
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Item added to cart" });
        }

        // POST: api/Cart/RemoveFromCart
        [HttpPost("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCartApi([FromBody] CartViewModel model)
        {
            var userId = User.Identity.Name; // Kullanıcının kimliğini alın
            var cartItem = _context.CartItems.SingleOrDefault(c => c.BookId == model.BookId && c.UserId == userId);

            if (cartItem == null)
            {
                return NotFound(new { Message = "Item not found in cart" });
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Item removed from cart" });
        }

        // POST: api/Cart/CheckoutConfirmed
        [HttpPost("CheckoutConfirmed")]
        public async Task<IActionResult> CheckoutConfirmedApi()
        {
            var userId = User.Identity.Name; // Kullanıcının kimliğini alın
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();

            if (cartItems.Count == 0)
            {
                return BadRequest(new { Message = "Cart is empty" });
            }

            // Sipariş işleme kodunu buraya ekleyin

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Order confirmed" });
        }
    }
}
