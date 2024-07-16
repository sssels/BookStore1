using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookStore1.Data;
using BookStore1.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookStore1.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ApplicationDbContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _context.Carts
                .FirstOrDefault(c => c.UserId == userId);
            
            if (cart == null)
            {
                _logger.LogWarning("Cart not found for user: {UserId}", userId);
                return RedirectToAction("Index", "Home");
            }

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutConfirmed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _context.Carts
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                _logger.LogWarning("Cart not found for user: {UserId}", userId);
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Book.Price)
                };

                _context.Orders.Add(order);

                foreach (var cartItem in cart.CartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        Order = order,
                        BookId = cartItem.BookId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Book.Price
                    };

                    _context.OrderDetails.Add(orderDetail);
                }

                _context.Carts.Remove(cart);
                _context.SaveChanges();

                _logger.LogInformation("Order created successfully for user: {UserId}", userId);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the order.");
                return RedirectToAction("Index", "Home"); // Or handle the error appropriately
            }
        }
    }
}
