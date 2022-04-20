namespace boomoseries_OrchAuth_api.Models
{
    public class UserBookPreferenceModel
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Pages { get; set; }
        public double Rating { get; set; }
    }
}
