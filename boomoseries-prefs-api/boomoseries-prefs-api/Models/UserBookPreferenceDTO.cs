namespace boomoseries_prefs_api.Models
{
    public class UserBookPreferenceDTO
    {
        public int Userid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Pages { get; set; }
        public double Rating { get; set; }
    }
}
