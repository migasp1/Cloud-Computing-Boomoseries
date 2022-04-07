using System.Collections.Generic;

namespace boomoseries_IMDB_api
{
    public class IMDBWatchable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Cast { get; set; }
    }
}
