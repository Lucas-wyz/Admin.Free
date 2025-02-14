using Admin.Free;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
