using Microsoft.EntityFrameworkCore;
using BookStore1.Data;
using BookStore1.Models;
#nullable disable
public class PublisherRepository : IPublisherRepository
{
    private readonly ApplicationDbContext _context;

    public PublisherRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
    {
        return await _context.Bookz
            .Select(b => new Publisher { Name = b.Publisher })
            .Distinct()
            .ToListAsync();
    }

    public async Task<Publisher> GetPublisherByIdAsync(int id)
    {
        return await _context.Publishers.FindAsync(id);
    }

    public async Task AddPublisherAsync(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePublisherAsync(Publisher publisher)
    {
        _context.Publishers.Update(publisher);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePublisherAsync(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);
        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();
    }
}
