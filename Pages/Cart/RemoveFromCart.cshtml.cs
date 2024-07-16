using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookStore1.Data;
using BookStore1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
#nullable disable
namespace BookStore1.Pages.Cart
{
    public class RemoveFromCartModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RemoveFromCartModel> _logger;

        public RemoveFromCartModel(ApplicationDbContext context, ILogger<RemoveFromCartModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public int BookId { get; set; }

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
                var cart = await _context.Carts
                    .Include(c => c.CartItems) // Ensure CartItems are included
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart != null)
                {
                    var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == BookId);
                    if (cartItem != null)
                    {
                        _context.CartItems.Remove(cartItem);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Book removed from cart successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the book from the cart.");
                return RedirectToPage("/Error");
            }

            return RedirectToPage("./Cart");
        }
    }
}
