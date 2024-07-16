using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using BookStore1.Data;
using BookStore1.Models;
#nullable disable
namespace BookStore1.Pages.Cart
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

        public IActionResult OnGet()
        {
            var userId = User.Identity.Name; // Example of retrieving the user ID, adjust as necessary
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User ID is null or empty.");
                return RedirectToPage("/Error"); // Or handle the error appropriately
            }

            try
            {
                var cart = _context.Carts
                    .Where(c => c.UserId == userId)
                    .FirstOrDefault();

                if (cart == null)
                {
                    // Handle case where cart is not found for the user
                    return RedirectToPage("/Error"); // Or redirect to a page indicating no cart found
                }

                // Optionally, load cart items here if needed

                // Handle the retrieved cart (e.g., assign it to a property for the view)
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the cart.");
                return RedirectToPage("/Error"); // Or handle the error appropriately
            }

            return Page();
        }
    }
}
