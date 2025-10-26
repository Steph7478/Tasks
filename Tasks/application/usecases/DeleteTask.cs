using Tasks.Domain.Repositories;
using Tasks.Domain.Services;

namespace Tasks.Application.Usecases
{
    public class DeleteTaskUseCase(ITaskRepository taskRepository, TaskDomainService taskDomainService)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly TaskDomainService _taskDomainService = taskDomainService;

        public async Task ExecuteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException("Task not found");

            _taskDomainService.DeleteTask(task);

            await _taskRepository.DeleteAsync(task);
        }
    }
}
