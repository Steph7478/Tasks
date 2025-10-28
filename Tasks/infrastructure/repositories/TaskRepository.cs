using Tasks.Domain.Repositories;
using DomainTask = Tasks.Domain.Entities.Task;
using Tasks.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Tasks.Infrastructure.Mappers;
using Tasks.Infrastructure.Entities;

namespace Tasks.Infrastructure.Repositories
{
    public class TaskRepository(AppDbContext context) : ITaskRepository
    {
        private readonly AppDbContext _context = context;

        // Add
        public async Task AddAsync(DomainTask task)
        {
            TaskEntity entity = TaskMapper.ToEntity(task);
            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync();

            typeof(DomainTask).GetProperty("Id")!.SetValue(task, entity.Id);
        }

        // Update
        public async Task UpdateAsync(DomainTask domain)
        {
            TaskEntity entity = await _context.Tasks.FindAsync(domain.Id)
                         ?? throw new KeyNotFoundException("Task not found");

            entity.Title = domain.Title;
            entity.Description = domain.Description;
            entity.CurrentStatus = domain.CurrentStatus;

            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(DomainTask domain)
        {
            TaskEntity entity = await _context.Tasks.FindAsync(domain.Id)
                         ?? throw new KeyNotFoundException("Task not found");

            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }

        // GetById
        public async Task<DomainTask> GetByIdAsync(Guid id)
        {
            TaskEntity entity = await _context.Tasks.FindAsync(id)
                         ?? throw new KeyNotFoundException("Task not found");

            return TaskMapper.ToDomain(entity);
        }

        // GetAll
        public async Task<List<DomainTask>> GetAllAsync()
        {
            List<TaskEntity> entities = await _context.Tasks.ToListAsync();
            return [.. entities.Select(TaskMapper.ToDomain)];
        }

        // ExistsByTitle
        public async Task<bool> ExistsByTitleAsync(string title)
        {
            return await _context.Tasks.AnyAsync(t => t.Title == title);
        }
    }
}
