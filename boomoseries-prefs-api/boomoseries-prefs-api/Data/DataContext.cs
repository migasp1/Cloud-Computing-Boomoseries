using boomoseries_prefs_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace boomoseries_prefs_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserBookPreference> UserBooksPreferences { get; set; }
        public DbSet<UserWatchablePreference> UserWatchablesPreferences { get; set; }

    }
}
