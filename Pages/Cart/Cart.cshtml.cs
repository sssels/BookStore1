using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using BookStore1.Models;
using BookStore1.Services;
#nullable disable
namespace BookStore1.Pages
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

        public BookStore1.Models.Cart Cart { get; set; } // Fully qualify the Cart type

        public void OnGet()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogError("User ID is null or empty.");
                    // Return an appropriate response or error page
                    RedirectToPage("/Error"); 
                    return;
                }

                Cart = _cartService.GetCart(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the cart.");
                // Return an error page
                RedirectToPage("/Error");
            }
        }

        public IActionResult OnPostRemoveFromCart(int bookId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _cartService.RemoveFromCart(userId, bookId);
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
                _cartService.ClearCart(userId);
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
                _cartService.Checkout(userId);
                return RedirectToPage("/Index"); // Redirect to the home page after checkout
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the checkout process.");
                return RedirectToPage("/Error");
            }
        }
    }
}
