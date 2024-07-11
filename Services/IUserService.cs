using BookStore1.Models;
#nullable disable
namespace BookStore1.Services
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> Login(string username, string password);
        Task<User> AdminLogin(string username, string password);
    }
}
