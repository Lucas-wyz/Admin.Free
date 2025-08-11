using Admin.Free.Infra;
using Admin.Free.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Admin.Free
{

    public class AppDbContext : DbContext
    {
        private readonly HttpContext _httpContext;
        private readonly string _tenantId;
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor, ILogger<AppDbContext> logger) : base(options)
        {
			_httpContext = httpContextAccessor.HttpContext;
			_tenantId = _httpContext.Request.Headers["TenantId"];
        }

        public DbSet<Permissions> Permissions => Set<Permissions>();
		public DbSet<Accounts> Accounts => Set<Accounts>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<AnonymousUsers> AnonymousUsers => Set<AnonymousUsers>();
        public DbSet<Roles> Roles => Set<Roles>();
        public DbSet<RolePermission> RolePermission => Set<RolePermission>();
		public DbSet<Questions> Questions => Set<Questions>();
		public DbSet<QuestionOptions> QuestionOptions => Set<QuestionOptions>();
		public DbSet<QuestionHistory> QuestionHistory => Set<QuestionHistory>();
		public DbSet<UserRole> UserRole => Set<UserRole>();
		public DbSet<Exams> Exams => Set<Exams>();
        public DbSet<ExamsQuertion> ExamsQuertion => Set<ExamsQuertion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			//modelBuilder
			foreach (var item in modelBuilder.Model.GetEntityTypes())
			{
				var type = item.ClrType;
                var expr = ExpressionableEX.Create(type);
                expr.AndAlso(nameof(ModelBase.TenantId), _tenantId);
                expr.AndAlso(nameof(ModelBase.IsDeleted), false);

                modelBuilder.Entity(type).HasQueryFilter(expr.ToLambda());
			}
        }

    }
}
