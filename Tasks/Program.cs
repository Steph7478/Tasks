using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Config;
using DotNetEnv;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "dev";

Env.Load($".env.{environment}");

var builderConfig = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables();

var config = builderConfig.Build();

bool useInMemory = environment == "test";
var options = DatabaseConfig.GetDbOptions(config, useInMemory);

using (var context = new AppDbContext(options))
{
    context.Database.EnsureCreated();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(_ => new AppDbContext(options));

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Server running!");
app.MapControllers();

app.Run("http://localhost:5000");
