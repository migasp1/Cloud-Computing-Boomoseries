namespace bomoseries_Disney_api.DTOs
{
    public class WatchableDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public float Rating { get; set; }
        public string Platform { get; set; }
    }
}
