using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookStore1.Services;
using BookStore1.Models;
using System.Threading.Tasks;
#nullable disable
namespace BookStore1.Pages.Cart
{
    public class AddToCartModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly ILogger<AddToCartModel> _logger;

        public AddToCartModel(ICartService cartService, ILogger<AddToCartModel> logger)
        {
            _cartService = cartService;
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

            var userId = User.Identity.Name; // Example of retrieving the user ID, adjust as necessary
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User ID is null or empty.");
                return RedirectToPage("/Error"); // Or handle the error appropriately
            }

            try
            {
                _cartService.AddToCart(userId, Input.BookId, Input.Title, Input.Price, Input.Quantity);
                _logger.LogInformation("Book added to cart successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the book to the cart.");
                return RedirectToPage("/Error"); // Or handle the error appropriately
            }

            return RedirectToPage("./Cart");
        }
    }
}
