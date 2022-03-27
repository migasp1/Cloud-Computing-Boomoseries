namespace boomoseries_IMDB_api
{
    public class IMDBWatchable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public float Rating { get; set; }
    }
}
