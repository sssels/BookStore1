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
            var booksQuery = from b in _context.Books
                             select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Başlık veya yazar adına göre arama yap
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchString) ||
                    b.Author.Contains(searchString));
            }

            var books = await booksQuery.ToListAsync();
            return Ok(books);
        }
    }
}
