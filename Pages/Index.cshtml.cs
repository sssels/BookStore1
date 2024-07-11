using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
#nullable disable
public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Book> Books { get; private set; }

    public void OnGet()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();

                string query = "SELECT * FROM books;";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Books = new List<Book>();
                    while (reader.Read())
                    {
                        Books.Add(new Book
                        {
                            Title = reader["Title"].ToString(),
                            Author = reader["Author"].ToString(),
                            PublicationYear = Convert.ToInt32(reader["PublicationYear"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}

public class Book
{
    internal string Genre;
    internal int InStock;
    internal DateTime UpdateDate;
    internal DateTime CreateDate;

    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    public int ID { get; internal set; }
    public decimal Price { get; internal set; }
}
