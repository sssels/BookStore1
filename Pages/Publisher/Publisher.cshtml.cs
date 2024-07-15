using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore1.Models;
#nullable disable
namespace BookStore1.Pages
{
    public class PublisherModel : PageModel
    {
        private readonly IPublisherService _publisherService;

        public PublisherModel(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public IEnumerable<Publisher> Publishers { get; set; }

        public async Task OnGetAsync()
        {
            Publishers = await _publisherService.GetAllPublishersAsync();
        }
    }
}
