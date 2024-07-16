using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookStore1.Services;
using System;
using System.Threading.Tasks;
#nullable disable
namespace BookStore1.Pages.Cart
{
    public class RemoveFromCartModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly ILogger<RemoveFromCartModel> _logger;

        public RemoveFromCartModel(ICartService cartService, ILogger<RemoveFromCartModel> logger)
        {
            _cartService = cartService;
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

            var userId = User.Identity.Name; // Example of retrieving the user ID, adjust as necessary
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User ID is null or empty.");
                return RedirectToPage("/Error"); // Or handle the error appropriately
            }

            try
            {
                _cartService.RemoveFromCart(userId, BookId);
                _logger.LogInformation("Book removed from cart successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the book from the cart.");
                return RedirectToPage("/Error"); // Or handle the error appropriately
            }

            return RedirectToPage("./Cart");
        }
    }
}
