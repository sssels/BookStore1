using System;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BookStore1.Models;
public class CartItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}
