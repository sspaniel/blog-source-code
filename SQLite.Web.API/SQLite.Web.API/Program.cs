using Microsoft.EntityFrameworkCore;
using SQLite.Web.API.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqliteDbFilePath = builder.Configuration["SQLite-DbFilePath"];

if (builder.Configuration["Environment"] == "local")
{
    var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    sqliteDbFilePath = $"{currentDirectory}{sqliteDbFilePath}";
}

if (!File.Exists(sqliteDbFilePath))
{
    Directory.CreateDirectory(Path.GetDirectoryName(sqliteDbFilePath));
}

builder.Services.AddDbContext<EFCoreRepository>(options =>
    options.UseSqlite($"Data Source={sqliteDbFilePath}"));

builder.Services.AddScoped<IRepository, EFCoreRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<EFCoreRepository>();
    repository.Database.Migrate();
}

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
