using boomosseries_GoodReads_api;
using Microsoft.EntityFrameworkCore;

namespace boomosseries_GoodReads_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Books> Books { get; set; }
    }
}
