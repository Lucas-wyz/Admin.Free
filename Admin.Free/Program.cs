using Admin.Free;
using Admin.Free.Filter;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Admin.Free.Models;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilterAttribute>();
}).AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(jwtOptions =>
	{

		jwtOptions.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuer = false, //是否验证Issuer
			ValidIssuer = JwtConfig.issuer,//configuration["Jwt:Issuer"], //发行人Issuer
			ValidateAudience = true, //是否验证Audience
			ValidAudience = JwtConfig.audience,// configuration["Jwt:Audience"], //订阅人Audience
			ValidateIssuerSigningKey = true, //是否验证SecurityKey
			IssuerSigningKey = new SymmetricSecurityKey(JwtConfig.secretKeyBytes), //SecurityKey

			ValidateLifetime = false, //是否验证失效时间
			ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
			RequireExpirationTime = true,//必须具有“过期”值
		};

	});


//Serilog
builder.Services.AddSerilog((service, logger) =>
{
    logger.WriteTo.Console();
});

//mongodb for ef
builder.Services.AddDbContext<AppDbContext>((service, options) =>
{
	options.UseMongoDB(service.GetService<IConfiguration>().GetConnectionString("mongodb"), "appdb");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
