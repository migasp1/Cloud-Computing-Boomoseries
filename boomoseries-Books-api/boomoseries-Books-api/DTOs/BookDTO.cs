namespace boomoseries_Books_api.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Pages { get; set; }
        public double Rating { get; set; }
    }
}
