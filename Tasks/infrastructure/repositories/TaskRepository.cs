using Tasks.Domain.Repositories;
using Tasks.Infrastructure.Mappers;
using DomainTask = Tasks.Domain.Entities.Task;
using Microsoft.EntityFrameworkCore;
using Tasks.Infrastructure.Context;

namespace Tasks.Infrastructure.Repositories
{
    public class TaskRepositoryAdapter(AppDbContext context) : ITaskRepository
    {
        private readonly AppDbContext context = context;

        // Domain → Infra via Mapper
        public async Task AddAsync(DomainTask task)
        {
            var entity = TaskMapper.ToEntity(task);
            context.Tasks.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DomainTask task)
        {
            var entity = TaskMapper.ToEntity(task);
            context.Tasks.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DomainTask task)
        {
            var entity = TaskMapper.ToEntity(task);
            context.Tasks.Remove(entity);
            await context.SaveChangesAsync();
        }

        // Infra → Domain via Mapper
        public async Task<DomainTask?> GetByIdAsync(Guid id)
        {
            var entity = await context.Tasks.FindAsync(id);
            return entity == null ? null : TaskMapper.ToDomain(entity);
        }

        public async Task<List<DomainTask>> GetAllAsync()
        {
            var entities = await context.Tasks.ToListAsync();
            return [.. entities.Select(TaskMapper.ToDomain)];
        }

        public async Task<bool> ExistsByTitleAsync(string title)
        {
            return await context.Tasks.AnyAsync(t => t.Title == title);
        }
    }
}
