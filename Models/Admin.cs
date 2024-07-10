using System.ComponentModel.DataAnnotations;
namespace BookStore1.Models;
#nullable disable
public class Admin
{
    [Key]
    public int AdminId { get; set; }
    public string AdminName { get; set; }
    public string AdminPass { get; set; }
    public string Role { get; set; }

}
