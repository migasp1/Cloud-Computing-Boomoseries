namespace boomoseries_prefs_api.Entities
{
    public class UserWatchablePreference
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public double Rating { get; set; }
        public string Type { get; set; }
        public string Platform { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
    }
}
