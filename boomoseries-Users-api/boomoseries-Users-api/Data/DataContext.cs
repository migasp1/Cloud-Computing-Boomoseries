using boomoseries_Users_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace boomoseries_Users_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
