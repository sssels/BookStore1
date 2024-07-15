using BookStore1.Models;
public interface IPublisherService
{
    Task<IEnumerable<Publisher>> GetAllPublishersAsync();
    Task<Publisher> GetPublisherByIdAsync(int id);
    Task AddPublisherAsync(Publisher publisher);
    Task UpdatePublisherAsync(Publisher publisher);
    Task DeletePublisherAsync(int id);
}
