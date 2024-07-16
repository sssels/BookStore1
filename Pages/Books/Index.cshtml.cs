using BookStore1.Data;
using BookStore1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace BookStore1.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Bookz { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Bookz = await _context.Books.ToListAsync();
            return Page();
        }
    }
}
