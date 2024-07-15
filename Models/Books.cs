
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BookStore1.Models
{
    public class Book
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required]
        [MaxLength(200)] // Optional: Set a maximum length
        public string Title { get; set; }

        [Required]
        [MaxLength(100)] // Optional: Set a maximum length
        public string Author { get; set; }
        [Required]
        [MaxLength(100)] // Optional: Set a maximum length
        public string Genre { get; set; }
        [Required]
        [MaxLength(100)] // Optional: Set a maximum length
        public string Publisher { get; set; }

        [Required]
        [Range(0, double.MaxValue)] // Ensure price is non-negative
        public decimal Price { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }
        
        public int InStock { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

    }
}
