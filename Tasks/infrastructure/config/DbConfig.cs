using Microsoft.EntityFrameworkCore;
using Tasks.Infrastructure.Context;

namespace Tasks.Infrastructure.Config
{
    public static class DatabaseConfig
    {
        public static DbContextOptions<AppDbContext> GetDbOptions(IConfiguration config, bool useInMemory = false)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            if (useInMemory)
                optionsBuilder.UseInMemoryDatabase("TasksTestDb");
            else
            {
                var connectionString = config.GetConnectionString("TasksDb");
                optionsBuilder.UseSqlite(connectionString);
            }

            return optionsBuilder.Options;
        }
    }
}
