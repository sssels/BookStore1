using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore1.Data;
using BookStore1.Models;

namespace BookStore1.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please correct the errors in the form.";
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            var user = new ApplicationUser
            {
                UserName = Username,
                Email = Email
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                // Kullanıcı başarıyla kaydedildi, şimdi oturum açalım
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Kullanıcının Id değerine erişmek için user.Id kullanabiliriz
                var userId = user.Id;

                // Başarı mesajı ayarla
                SuccessMessage = $"Registration successful! UserId: {user.Id}";

                return RedirectToPage("/Index"); // Örnek olarak ana sayfaya yönlendir
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
