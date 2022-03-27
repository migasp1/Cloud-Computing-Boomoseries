using Microsoft.EntityFrameworkCore;

namespace boomoseries_Netflix_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<watchable> Watchables { get; set; }

    }
}
