using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookStore1.Data;
using BookStore1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
#nullable disable
namespace BookStore1.Pages.Cart
{
    public class AddToCartModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AddToCartModel> _logger;

        public AddToCartModel(ApplicationDbContext context, ILogger<AddToCartModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public CartItem Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.Identity.Name;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User ID is null or empty.");
                return RedirectToPage("/Error");
            }

            try
            {
                // Kullanıcının sepetini al
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    cart = new BookStore1.Models.Cart { UserId = userId };

                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

                // Yeni bir CartItem oluştur
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == Input.BookId);
                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        CartId = cart.Id,
                        BookId = Input.BookId,
                        Title = Input.Title,
                        Price = Input.Price,
                        Quantity = Input.Quantity
                    };
                    _context.CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += Input.Quantity;
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Book added to cart successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the book to the cart.");
                return RedirectToPage("/Error");
            }

            return RedirectToPage("./Cart");
        }
    }
}
