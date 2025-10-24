using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Tasks.Infrastructure.Config;
using Tasks.Infrastructure.Context;

namespace Tasks.Infrastructure.Config
{
    public class EnvConfig
    {
        public IConfiguration Configuration { get; private set; }
        public string Environment { get; private set; }
        public bool UseInMemoryDatabase { get; private set; }

        public EnvConfig()
        {
            Environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            Env.Load($".env.{Environment}");

            UseInMemoryDatabase = Environment.Equals("Test", StringComparison.OrdinalIgnoreCase);

            var builderConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builderConfig.Build();
        }

        public DbContextOptions<AppDbContext> GetDbOptions()
        {
            return DatabaseConfig.GetDbOptions(Configuration, UseInMemoryDatabase);
        }
    }
}
