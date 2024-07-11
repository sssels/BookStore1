using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore1.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostRegister()
        {
            return RedirectToPage("/Register");
        }

        public IActionResult OnPostLogin()
        {
            return RedirectToPage("/Login");
        }

        public IActionResult OnPostCart()
        {
            return RedirectToPage("/Cart");
        }
    }
}
