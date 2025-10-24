using Microsoft.Extensions.Configuration;
using Tasks.Infrastructure.Config;
using DotNetEnv;
using Tasks.Infrastructure.Context;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "dev";

Env.Load($".env.{environment}");

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables();

var config = builder.Build();

bool useInMemory = environment == "test";
var options = DatabaseConfig.GetDbOptions(config, useInMemory);

using var context = new AppDbContext(options);
context.Database.EnsureCreated();
