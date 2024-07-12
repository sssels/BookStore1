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
            return RedirectToPage("/Account/Register");
        }

        public IActionResult OnPostLogin()
        {
            return RedirectToPage("/Account/Login");
        }

        public IActionResult OnPostViewBooks()
        {
            return RedirectToPage("/Books/Index");
        }

        public IActionResult OnPostCart()
        {
            return RedirectToPage("/Cart/Index");
        }
    }
}
