using Admin.Free.Models;
using Microsoft.EntityFrameworkCore;

namespace Admin.Free
{

    public class AppDbContext : DbContext
    {
		private HttpContext _httpContext { get; set; }
		private string _tenantId { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
			_httpContext = httpContextAccessor.HttpContext;
			_tenantId = _httpContext.Request.Headers["TenantId"];
        }

        public DbSet<Users> Users => Set<Users>();
        public DbSet<Roles> Roles => Set<Roles>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Users>().HasQueryFilter(x => x.TenantId == _tenantId).HasQueryFilter(x => x.IsDeleted == false);
        }

    }
}
