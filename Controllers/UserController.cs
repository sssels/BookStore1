using Microsoft.AspNetCore.Mvc;
using BookStore1.Models;
using BookStore1.Services;
using System.Threading.Tasks;
using BookStore1.Data;
#nullable disable
namespace BookStore1.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email))
        {
            return BadRequest("Invalid user data.");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }
}
}