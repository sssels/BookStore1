using BookStore1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
}
