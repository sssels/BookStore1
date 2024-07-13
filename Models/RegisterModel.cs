#nullable disable
namespace BookStore1.Pages.Account
{
    public class InputModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        // Diğer alanları ekleyebilirsiniz, örneğin şifre doğrulama için:
        public string ConfirmPassword { get; set; }
    }
}
