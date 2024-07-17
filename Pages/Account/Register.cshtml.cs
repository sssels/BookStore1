using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookStore1.Pages
{
    public class RegModel : PageModel
    {
        private readonly ILogger<RegModel> _logger;

        public RegModel(ILogger<RegModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = "";

        public string successMessage = "";
        public string errorMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                errorMessage = "Data validation failed: " + string.Join("; ", errors);
                _logger.LogError(errorMessage);
                return;
            }

            successMessage = "Successfully registered";
            _logger.LogInformation(successMessage);
            Username = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
            ModelState.Clear();
        }
    }
}
