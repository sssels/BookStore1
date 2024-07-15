using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BookStore1.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Eğer ek özellikler varsa buraya ekleyebilirsiniz.
    }
}
