using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore1.Models;
using BookStore1.Services;
#nullable disable
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var newUser = await _userService.Register(user);
        return Ok(newUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var loggedInUser = await _userService.Login(user.Username, user.Password);
        if (loggedInUser == null)
        {
            return Unauthorized();
        }
        return Ok(loggedInUser);
    }

    [HttpPost("admin-login")]
    public async Task<IActionResult> AdminLogin([FromBody] User user)
    {
        var adminUser = await _userService.AdminLogin(user.Username, user.Password);
        if (adminUser == null)
        {
            return Unauthorized();
        }
        return Ok(adminUser);
    }
}
