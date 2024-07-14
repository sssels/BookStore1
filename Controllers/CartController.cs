using BookStore1.Models;
using BookStore1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
#nullable disable

namespace BookStore1.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _cartService.GetCart(userId);
            return View(cart);
        }

        public IActionResult AddToCart(int bookId, string title, decimal price, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.AddToCart(userId, bookId, title, price, quantity);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.RemoveFromCart(userId, bookId);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.ClearCart(userId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.Checkout(userId);
            return RedirectToAction("Index", "Home");
        }
    }
}
