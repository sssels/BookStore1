using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore1.Models;
using BookStore1.Services;

namespace BookStore1.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new User
            {
                Username = Input.Username,
                Email = Input.Email,
                Password = Input.Password // Bu kısmın şifreleme işlemiyle güvenli hale getirilmesi önemlidir.
            };

            await _userService.CreateUserAsync(user);
            return RedirectToPage("/Index");
        }
    }
}
