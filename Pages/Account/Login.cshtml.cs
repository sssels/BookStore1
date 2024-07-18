using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore1.Models;

namespace BookStore1.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoginModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new System.Uri("https://localhost:5289/"); // API base URL
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonSerializer.Serialize(Input), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/account/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Message = "Login successful!";
                // Redirect to a different page or perform other actions as needed
                return RedirectToPage("/Index"); // Redirect to home page or another page
            }
            else
            {
                Message = "Invalid username or password";
                return Page();
            }
        }
    }
}
