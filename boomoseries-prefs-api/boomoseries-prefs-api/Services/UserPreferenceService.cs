using boomoseries_prefs_api.Data;
using boomoseries_prefs_api.Entities;
using boomoseries_prefs_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace boomoseries_prefs_api.Services
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private DataContext _context;
        public UserPreferenceService(DataContext context)
        {
            _context = context;
        }

        public void AddUserWatchbleFavorite(UserWatchablePreference userWatchableFavorite)
        {
            if (userWatchableFavorite == null)
            {
                throw new Exception("Invalid request");
            }
            var userSpecificWatchables = _context.UserWatchablesPreferences.Where(w => w.Userid == userWatchableFavorite.Userid);

            if (userSpecificWatchables.Where(w => w.Title == userWatchableFavorite.Title && w.Type == userWatchableFavorite.Type).Any())
            {
                throw new Exception("Already added watchable to favorites");
            }
            else
            {
                _context.UserWatchablesPreferences.Add(userWatchableFavorite);
                _context.SaveChanges();
            }
        }

        public void AddUserBookFavorite(UserBookPreference userBookFavorite)
        {
            if (userBookFavorite == null)
            {
                throw new Exception("Invalid request");
            }
            var userSpecificBooks = _context.UserBooksPreferences.Where(w => w.Userid == userBookFavorite.Userid);

            if (userSpecificBooks.Where(b => b.Title.ToLower() == userBookFavorite.Title.ToLower()).Any())
            {
                throw new Exception("Already added book to favorites");
            }
            else
            {
                _context.UserBooksPreferences.Add(userBookFavorite);
                _context.SaveChanges();
            }
        }

        public List<UserBookPreference> GetUserFavoriteBooks(int userId)
        {
            var userSpecificBooks = _context.UserBooksPreferences.Where(b => b.Userid == userId);

            if (userSpecificBooks is null)
            {
                throw new Exception("User Id does not match");
            }
            return userSpecificBooks.ToList();
        }

        public List<UserWatchablePreference> GetUserFavoriteWatchables(int userId)
        {
            var userSpecificWatchables = _context.UserWatchablesPreferences.Where(b => b.Userid == userId);

            if (userSpecificWatchables is null)
            {
                throw new Exception("User Id does not match");
            }
            return userSpecificWatchables.ToList();
        }
    }
}
