using Microsoft.AspNetCore.Identity;
#nullable disable
namespace BookStore1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string name { get; set; } 

        public string location {get; set;}         

    }
}