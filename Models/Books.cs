using System;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BookStore1.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }

        // Parametresiz constructor
        public Book()
        {
        }

        // Parametreli constructor
        public Book(int id, string author, string genre, string title, decimal price, int inStock, DateTime updated, DateTime created)
        {
            this.ID = id;
            this.Author = author;
            this.Title = title;
            this.Genre = genre;
            this.Price = price;
            this.InStock = inStock;
            this.UpdateDate = updated;
            this.CreateDate = created;
        }
    }
}