using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore1.Data;
using System.Threading.Tasks;
#nullable disable
namespace BookStore1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // ViewData yalnızca okuma özelliğidir, bu nedenle sadece veri eklenebilir
            ViewData["Title"] = "Home Page";
        }
    }
}
