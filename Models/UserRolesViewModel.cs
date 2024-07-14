using System.Collections.Generic;
#nullable disable
namespace BookStore1.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
