using Microsoft.EntityFrameworkCore;
using Tasks.Infrastructure.Context;

namespace Tasks.Tests
{
    public static class TestDbContextFactory
    {
        public static AppDbContext CreateInMemoryDbContext(string dbName = "TasksTestDb")
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}
