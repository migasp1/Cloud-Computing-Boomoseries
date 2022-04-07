namespace boomosseries_GoodReads_api.DTOs
{
    public class BooksDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Pages { get; set; } = string.Empty;
        public double Rating { get; set; }
    }
}
