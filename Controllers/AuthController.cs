using Microsoft.AspNetCore.Mvc;
using BookStore1.Models;

namespace BookStore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Örnek kullanıcı adı ve parola
            string correctUsername = "admin";
            string correctPassword = "password";

            if (loginModel.Username == correctUsername && loginModel.Password == correctPassword)
            {
                return Ok(new { message = "Giriş başarılı!" });
            }
            else
            {
                return Unauthorized(new { message = "Kullanıcı adı veya parola hatalı!" });
            }
        }
    }
}
