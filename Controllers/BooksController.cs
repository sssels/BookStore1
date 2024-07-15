using BookStore1.Data;
using BookStore1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
#nullable disable
namespace BookStore1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Bookz.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Author,Title,Genre,Price,InStock,Publisher,AddedDate,UpdatedDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var book = await _context.Bookz.FindAsync(id);
    if (book == null)
    {
        return NotFound();
    }
    return View(book);
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Author,Title,Genre,Price,InStock,Publisher,AddedDate,UpdatedDate")] Book book)
        {
            if (id != book.Id)
        {
        return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        } 
        return View(book);
}

        private bool BookExists(int id)
        {   
            return _context.Bookz.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var book = await _context.Bookz
        .FirstOrDefaultAsync(m => m.Id == id);
    if (book == null)
    {
        return NotFound();
    }

    return View(book);
}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Bookz.FindAsync(id);
            _context.Bookz.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}
