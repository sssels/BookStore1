using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using BookStore1.Data;
using BookStore1.Models;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace BookStore1.Pages
{
    public class CartModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartModel> _logger;

        public CartModel(ApplicationDbContext context, ILogger<CartModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public BookStore1.Models.Cart Cart { get; set; }

        public void OnGet()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogError("User ID is null or empty.");
                    RedirectToPage("/Error");
                    return;
                }

                Cart = _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Book)
                    .FirstOrDefault(c => c.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the cart.");
                RedirectToPage("/Error");
            }
        }

        public IActionResult OnPostRemoveFromCart(int bookId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cart = _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefault(c => c.UserId == userId);

                if (cart != null)
                {
                    var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
                    if (cartItem != null)
                    {
                        _context.CartItems.Remove(cartItem);
                        _context.SaveChanges();
                    }
                }

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the item from the cart.");
                return RedirectToPage("/Error");
            }
        }

        public IActionResult OnPostClearCart()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cart = _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefault(c => c.UserId == userId);

                if (cart != null)
                {
                    _context.CartItems.RemoveRange(cart.CartItems);
                    _context.SaveChanges();
                }

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while clearing the cart.");
                return RedirectToPage("/Error");
            }
        }

        public IActionResult OnPostCheckout()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cart = _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefault(c => c.UserId == userId);

                if (cart != null)
                {
                    // Burada ödeme işlemleri gerçekleştirilebilir

                    _context.CartItems.RemoveRange(cart.CartItems);
                    _context.SaveChanges();
                }

                return RedirectToPage("/Index"); // Checkout sonrası ana sayfaya yönlendirme
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the checkout process.");
                return RedirectToPage("/Error");
            }
        }
    }
}
