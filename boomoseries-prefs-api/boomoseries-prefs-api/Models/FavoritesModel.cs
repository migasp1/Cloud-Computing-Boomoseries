using boomoseries_prefs_api.Entities;
using System.Collections.Generic;

namespace boomoseries_prefs_api.Models
{
    public class FavoritesModel
    {
        public List<UserBookPreference> Books { get; set; }
        public List<UserWatchablePreference> Watchables { get; set; }
    }
}
