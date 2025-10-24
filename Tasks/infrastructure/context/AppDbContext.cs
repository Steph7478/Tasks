using Microsoft.EntityFrameworkCore;
using Tasks.Infrastructure.Entities;

namespace Tasks.Infrastructure.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks { get; set; } = null!;
    }
}
