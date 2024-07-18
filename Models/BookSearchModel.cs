using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BookStore1.Models;

namespace BookStore1.Pages.Search
{
    public class BookSearchModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public BookSearchModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Author { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Genre { get; set; }

        public List<Book> Books { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var query = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(Title))
            {
                query.Add("title", Title);
            }
            if (!string.IsNullOrEmpty(Author))
            {
                query.Add("author", Author);
            }
            if (!string.IsNullOrEmpty(Genre))
            {
                query.Add("genre", Genre);
            }

            var queryString = string.Join("&", query.Select(kv => $"{kv.Key}={kv.Value}"));
            var response = await _httpClient.GetAsync($"api/books/search?{queryString}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Books = JsonSerializer.Deserialize<List<Book>>(responseString);
                return new JsonResult(Books);
            }
            else
            {
                Books = new List<Book>();
                return new JsonResult(Books);
            }
        }
    }
}
