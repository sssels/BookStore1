using BookStore1.Models;
using System;
using System.Linq;
using BookStore1.Data;
namespace BookStore1.Services
{
    public interface IOrderService
    {
        void CreateOrder(string userId, Cart cart);
    }

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateOrder(string userId, Cart cart)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow
            };

            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    BookId = item.BookId,
                    Title = item.Title,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                order.OrderItems.Add(orderItem);

                // Kitap stoklarını güncelle
                var book = _context.Bookz.Find(item.BookId);
                if (book != null)
                {
                    book.InStock -= item.Quantity;
                    _context.SaveChanges(); // Veritabanında güncelleme yapılıyor
                }
                // Eğer stok kontrolü yapmak isterseniz burada ek bir kontrol ekleyebilirsiniz.
            }

            _context.Orders.Add(order);
            _context.SaveChanges(); // Sipariş veritabanına kaydediliyor

            // Sepeti temizle
            cart.Items.Clear();
        }
    }
}
