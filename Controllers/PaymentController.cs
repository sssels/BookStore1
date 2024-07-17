using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using BookStore1.Data;
using BookStore1.Models;

namespace BookStore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // HTTP POST api/payments
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            payment.PaymentDate = DateTime.UtcNow;

            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(payment.Amount * 100), // Stripe uses cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                // Ödeme işlemi başarılı ise veritabanına kaydet
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Payment processed successfully", PaymentId = payment.Id });
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun şekilde işle
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
