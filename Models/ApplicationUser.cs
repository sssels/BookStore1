using Microsoft.AspNetCore.Identity;
#nullable disable
namespace BookStore1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string username { get; set; } 
        public string email { get; set; }   
        public string password { get; set; }          

    }
}