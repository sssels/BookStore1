using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore1.Models;
using Microsoft.EntityFrameworkCore;
using BookStore1.Data;
using System.Linq;

#nullable disable
namespace BookStore1.Pages
{
    public class PublisherModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PublisherModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BookStore1.Models.Publisher> Publishers { get; set; }

        public async Task OnGetAsync()
        {
            Publishers = await _context.Publishers.ToListAsync();
        }
    }
}
