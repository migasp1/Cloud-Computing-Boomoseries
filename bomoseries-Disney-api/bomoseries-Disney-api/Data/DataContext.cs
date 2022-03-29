using Microsoft.EntityFrameworkCore;

namespace boomoseries_Disney_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Watchable> Watchables { get; set; }

    }
}
