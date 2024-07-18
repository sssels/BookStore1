using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore1.Data;
using BookStore1.Models;
using BookStore1.ViewModels;
using System.Linq;

#nullable disable

namespace BookStore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password // Note: Storing passwords as plain text is insecure. Use hashing in production.
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            // Return a successful login response
            return Ok(new { Message = "Login successful" });
        }
    }
}
