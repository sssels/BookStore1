using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore1.Models
{
    public class Payment
    {
        public int Id { get; set; }
        
        [Required]
        public string CardNumber { get; set; }
        
        [Required]
        public string CardHolderName { get; set; }
        
        [Required]
        public DateTime ExpirationDate { get; set; }
        
        [Required]
        public string CVV { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        public DateTime PaymentDate { get; set; }
    }
}
