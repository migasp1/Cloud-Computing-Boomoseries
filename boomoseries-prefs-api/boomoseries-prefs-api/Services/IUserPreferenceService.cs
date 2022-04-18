using boomoseries_prefs_api.Entities;
using System.Collections.Generic;

namespace boomoseries_prefs_api.Services
{
    public interface IUserPreferenceService
    {
        void AddUserWatchbleFavorite(UserWatchablePreference userWatchableFavorite);
        void AddUserBookFavorite(UserBookPreference userBookFavorite);
        List<UserBookPreference> GetUserFavoriteBooks(int userId);
        List<UserWatchablePreference> GetUserFavoriteWatchables(int userId);
    }
}
