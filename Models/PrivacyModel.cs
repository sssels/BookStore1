namespace BookStore1.Models
{
    public class PrivacyModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public PrivacyModel()
        {
            Title = "Privacy Policy";
            Content = "Use this page to detail your site's privacy policy.";
        }
    }
}
