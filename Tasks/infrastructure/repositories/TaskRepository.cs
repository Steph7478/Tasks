using Tasks.Domain.Repositories;
using DomainTask = Tasks.Domain.Entities.Task;
using Tasks.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Tasks.Infrastructure.Mappers;

namespace Tasks.Infrastructure.Repositories
{
    public class TaskRepository(AppDbContext context) : ITaskRepository
    {
        private readonly AppDbContext _context = context;

        // Add
        public async Task AddAsync(DomainTask task)
        {
            var entity = TaskMapper.ToEntity(task);
            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync();

            typeof(DomainTask).GetProperty("Id")!.SetValue(task, entity.Id);
        }

        // Update
        public async Task UpdateAsync(DomainTask domain)
        {
            var entity = _context.Tasks.Local.FirstOrDefault(t => t.Id == domain.Id)
                         ?? await _context.Tasks.FirstOrDefaultAsync(t => t.Id == domain.Id)
                         ?? throw new KeyNotFoundException("Task not found");

            entity.Title = domain.Title;
            entity.Description = domain.Description;
            entity.CurrentStatus = domain.CurrentStatus;

            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(DomainTask domain)
        {
            var entity = _context.Tasks.Local.FirstOrDefault(t => t.Id == domain.Id)
                         ?? await _context.Tasks.FirstOrDefaultAsync(t => t.Id == domain.Id)
                         ?? throw new KeyNotFoundException("Task not found");

            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }

        // GetById
        public async Task<DomainTask> GetByIdAsync(Guid id)
        {
            var entity = _context.Tasks.Local.FirstOrDefault(t => t.Id == id)
                         ?? await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id)
                         ?? throw new KeyNotFoundException("Task not found");

            return TaskMapper.ToDomain(entity);
        }

        // GetAll
        public async Task<List<DomainTask>> GetAllAsync()
        {
            var entities = await _context.Tasks.ToListAsync();
            return entities.Select(TaskMapper.ToDomain).ToList();
        }

        // ExistsByTitle
        public async Task<bool> ExistsByTitleAsync(string title)
        {
            return await _context.Tasks.AnyAsync(t => t.Title == title);
        }
    }
}
