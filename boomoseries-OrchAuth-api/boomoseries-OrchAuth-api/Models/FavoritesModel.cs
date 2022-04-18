using boomoseries_OrchAuth_api.Entities;
using System.Collections.Generic;

namespace boomoseries_OrchAuth_api.Models
{
    public class FavoritesModel
    {
        public List<UserBookPreferences> Books { get; set; }
        public List<UserWatchablesPreferences> Watchables { get; set; }
    }
}
