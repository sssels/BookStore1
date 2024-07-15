using BookStore1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookStore1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PublishersController : Controller
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return View(publishers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Publisher publisher) // Removed Address if not needed
        {
            if (ModelState.IsValid)
            {
                await _publisherService.AddPublisherAsync(publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _publisherService.GetPublisherByIdAsync(id.Value);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Publisher publisher) // Removed Address if not needed
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _publisherService.UpdatePublisherAsync(publisher);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PublisherExists(publisher.Id))
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
            return View(publisher);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _publisherService.GetPublisherByIdAsync(id.Value);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _publisherService.DeletePublisherAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PublisherExists(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            return publisher != null;
        }
    }
}
