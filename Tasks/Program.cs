using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Config;

// env configs
var envConfig = new EnvConfig();
var options = envConfig.GetDbOptions();

using (var context = new AppDbContext(options))
{
    context.Database.EnsureCreated();
}

// run server
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(_ => new AppDbContext(options));
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Server running!");
app.MapControllers();

app.Run("http://localhost:5000");
