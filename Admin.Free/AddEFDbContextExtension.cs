using Admin.Free;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

public static class AddEFDbContextExtension
{

    /// <summary>
    /// Extension EF DbContext
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddEFDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>((service, options) =>
        {
            var httpContextAccessor = service.GetService<IHttpContextAccessor>();

            var _httpContext = httpContextAccessor.HttpContext;
            var _isGet = _httpContext.Request.Method == HttpMethod.Get.Method;
            if (_isGet)
            {
                var conn = configuration.GetConnectionString("mongodbforbi");
                var serverVersion = MySqlServerVersion.AutoDetect(conn);
                //mysql for ef
                options.UseMySql(conn, serverVersion);
            }
            else
            {
                var conn = configuration.GetConnectionString("mongodb");
                var mongoUrl = MongoUrl.Create(conn);
                var dba = new MongoDB.Driver.MongoClient(mongoUrl);
                //mongodb for ef
                options.UseMongoDB(dba, mongoUrl.DatabaseName);
            }

        });


    }

}