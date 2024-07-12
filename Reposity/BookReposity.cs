using BookStore1.Data;
using BookStore1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
#nullable disable
public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Bookz.ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _context.Bookz.FindAsync(id);
    }

    public async Task AddBookAsync(Book book)
    {
        await _context.Bookz.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        _context.Bookz.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _context.Bookz.FindAsync(id);
        if (book != null)
        {
            _context.Bookz.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
