using Microsoft.EntityFrameworkCore;
using BookStore1.Models;
#nullable disable
namespace BookStore1.Data{
    public class ApplicationDbContext:DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options){

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

    }
}