using Admin.Free;
using Admin.Free.Filter;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Admin.Free.Models;
using Microsoft.IdentityModel.Tokens;
using Admin.Free.View;
using AutoMapper;

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

//EF
builder.Services.AddEFDbContext(builder.Configuration);


// AutoMapper
builder.Services.AddScoped<AutoMapper.IConfigurationProvider>(x => {
	var dbc = x.GetService<AppDbContext>();

	return new MapperConfiguration(cfg =>
			cfg.CreateMap<Users, UsersView>()
			.ForMember(x => x.RoleList, o => o.MapFrom(s => dbc.UserRole.Where(y => y.UserID == s.ID).Select(y => y.RoleID).ToList())));
			});

builder.Services.AddScoped<AutoMapper.IMapper>(x => { var dbc = x.GetService<AutoMapper.IConfigurationProvider>();
	return dbc.CreateMapper();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
