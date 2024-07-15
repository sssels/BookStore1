using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookStore1.Services;
#nullable disable
namespace BookStore1.Pages.Cart
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartModel> _logger;

        public CartModel(ICartService cartService, ILogger<CartModel> logger)
        {
            _cartService = cartService;
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
                var cart = _cartService.GetCart(userId);
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
