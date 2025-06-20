using Admin.Free.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

		public DbSet<Accounts> Accounts => Set<Accounts>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<Roles> Roles => Set<Roles>();
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
				var expression = QueryFilterBuild(type);

				modelBuilder.Entity(type).HasQueryFilter(expression);
			}
        }

		public LambdaExpression? QueryFilterBuild(Type type)
		{
			if (type.BaseType == typeof(ModelBase))
			{
				var param = Expression.Parameter(type);

				Expression<Func<string>> tenantLambda = () => _tenantId;
				var tenantParamExpression = tenantLambda.Body;
				var tenantEqualExpression = Expression.Equal(Expression.Property(param, nameof(ModelBase.TenantId)), tenantParamExpression);

				Expression<Func<bool?>> deletedParameterLambda1 = () => false;
				var deletedParamExpression1 = deletedParameterLambda1.Body;
				var deletedEqualExpression = Expression.Equal(Expression.Property(param, nameof(ModelBase.IsDeleted)), deletedParamExpression1);

				var andAlso = Expression.AndAlso(tenantEqualExpression, deletedEqualExpression);
				LambdaExpression whereLambda = Expression.Lambda(andAlso, param);
				return whereLambda;
			}
			else
			{
				return null;
			}
		}
    }
}
