using Admin.Free.Models;
using Microsoft.EntityFrameworkCore;

namespace Admin.Free
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users => Set<Users>();
        public DbSet<Roles> Roles => Set<Roles>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
