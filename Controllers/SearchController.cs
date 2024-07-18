using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore1.Data;
using BookStore1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookSearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookSearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // HTTP GET api/booksearch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks([FromQuery] string searchString)
        {
            // LINQ sorgusuyla kitapları ara
            var booksQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Başlık veya yazar adına göre arama yap (Büyük/küçük harf duyarlılığı yok)
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            var books = await booksQuery.ToListAsync();
            return Ok(books);
        }
    }
}
