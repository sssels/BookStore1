using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore1.Data;
using BookStore1.Models;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace BookStore1.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public class RegisterInputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }

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
                Password = Input.Password // Not: Şifreyi hashleyerek saklamalısınız.
            };

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, $"Veritabanı hatası: {ex.Message}");
                return Page();
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Genel hata: {ex.Message}");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
