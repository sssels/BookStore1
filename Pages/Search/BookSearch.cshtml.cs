using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore1.Data;
using BookStore1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore1.Pages
{
    public class BookSearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookSearchModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Books { get; set; }
        public string SearchString { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            SearchString = searchString;

            // LINQ sorgusuyla kitapları ara
            var booksQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Başlık veya yazar adına göre arama yap (Büyük/küçük harf duyarlılığı yok)
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            Books = await booksQuery.ToListAsync();
        }
    }
}
