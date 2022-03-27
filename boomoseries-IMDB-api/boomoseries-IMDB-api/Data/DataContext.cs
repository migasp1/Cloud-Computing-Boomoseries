using Microsoft.EntityFrameworkCore;

namespace boomoseries_IMDB_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<IMDBWatchable> Watchables { get; set; }
    }
}
