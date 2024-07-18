namespace BookStore1.Models
{
    public class AddToCartModel
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
    }

    public class RemoveFromCartModel
    {
        public string UserId { get; set; }
        public string Title { get; set; }
    }
}
