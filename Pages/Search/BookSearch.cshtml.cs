using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore1.Data;
using BookStore1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace BookStore1.Pages
{
    public class BookSearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookSearchModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Bookz { get; set; }
        public string SearchString { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            SearchString = searchString;

            // LINQ sorgusuyla kitapları ara
            var booksQuery = from b in _context.Bookz
                             select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Başlık veya yazar adına göre arama yap
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchString) ||
                    b.Author.Contains(searchString));
            }

            Bookz = await booksQuery.ToListAsync();
        }
    }
}
