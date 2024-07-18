namespace BookStore1.Models
{
    public class AddToCartInputModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class RemoveFromCartInputModel
    {
        public int BookId { get; set; }
    }
}
