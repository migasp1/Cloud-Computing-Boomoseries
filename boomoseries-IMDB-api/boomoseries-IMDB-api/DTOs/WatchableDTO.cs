namespace boomoseries_IMDB_api.DTOs
{
    public class WatchableDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public float Rating { get; set; }
        public string Cast { get; set; }
        public string Platform { get; set; }
    }
}
