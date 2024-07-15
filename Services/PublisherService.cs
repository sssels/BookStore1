using BookStore1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _publisherRepository;

    public PublisherService(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
    {
        return await _publisherRepository.GetAllPublishersAsync();
    }

    public async Task<Publisher> GetPublisherByIdAsync(int id)
    {
        return await _publisherRepository.GetPublisherByIdAsync(id);
    }

    public async Task AddPublisherAsync(Publisher publisher)
    {
        await _publisherRepository.AddPublisherAsync(publisher);
    }

    public async Task UpdatePublisherAsync(Publisher publisher)
    {
        await _publisherRepository.UpdatePublisherAsync(publisher);
    }

    public async Task DeletePublisherAsync(int id)
    {
        await _publisherRepository.DeletePublisherAsync(id);
    }
}
