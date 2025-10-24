using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(DomainTask task);
        Task UpdateAsync(DomainTask task);
        Task DeleteAsync(DomainTask task);
        Task<DomainTask?> GetByIdAsync(Guid id);
        Task<List<DomainTask>> GetAllAsync();
        Task<bool> ExistsByTitleAsync(string title);
    }
}
