using Maanslogen;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Maanslogen")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var host = Environment.GetEnvironmentVariable("PGHOST");
var port = Environment.GetEnvironmentVariable("PGPORT");
var user = Environment.GetEnvironmentVariable("PGUSER");
var password = Environment.GetEnvironmentVariable("PGPASSWORD");
var database = Environment.GetEnvironmentVariable("PGDATABASE");

var connectionString = $"Host={host};Port={port};Username={user};Password={password};Database={database};";
app.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var pendingMigrations = dbContext.Database.GetPendingMigrations();


    if (pendingMigrations.Any())
    {
        Console.WriteLine("Applying migrations...");
        dbContext.Database.Migrate();
    }
    else
    {
        Console.WriteLine("No migrations needed.");
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();