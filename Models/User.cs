using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace BookStore1.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Id { get; set; } // Id alanını ekleyin

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}