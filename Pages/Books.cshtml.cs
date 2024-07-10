using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore1.Models;
using BookStore1.Data;
#nullable disable
namespace BookStore1.Pages
{
    public class BooksModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BooksModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _context.Books.ToListAsync();
        }
    }
}