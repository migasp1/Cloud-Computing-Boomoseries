namespace boomoseries_Disney_api
{
    public class Watchable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public double Rating { get; set; }
    }
}