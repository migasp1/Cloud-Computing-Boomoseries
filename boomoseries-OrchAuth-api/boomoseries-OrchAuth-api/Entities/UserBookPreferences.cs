﻿namespace boomoseries_OrchAuth_api.Entities
{
    public class UserBookPreferences
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Pages { get; set; }
        public double Rating { get; set; }
    }

}
