using boomoseries_Amazon_api;
using Microsoft.EntityFrameworkCore;

namespace AmazonPrime_Microservice.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<watchable> Watchables { get; set; }

    }
}
