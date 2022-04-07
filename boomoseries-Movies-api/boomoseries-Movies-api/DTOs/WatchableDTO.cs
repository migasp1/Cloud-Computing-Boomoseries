namespace boomoseries_Movies_api.DTOs
{
    public class WatchableDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Platform { get; set; }
    }
}
