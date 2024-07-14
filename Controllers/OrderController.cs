using BookStore1.Models;
using BookStore1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
#nullable disable
namespace BookStore1.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public OrderController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _cartService.GetCart(userId);
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutConfirmed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _cartService.GetCart(userId);
            _orderService.CreateOrder(userId, cart);
            return RedirectToAction("Index", "Home");
        }
    }
}
