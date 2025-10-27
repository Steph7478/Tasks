using Tasks.Domain.Repositories;
using Tasks.Domain.Services;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Application.Usecases;

public class DeleteTaskUseCase(ITaskRepository taskRepository, TaskDomainService taskDomainService)
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly TaskDomainService _taskDomainService = taskDomainService;

    public async Task ExecuteAsync(DomainTask task)
    {
        _taskDomainService.ValidateDelete(task);
        await _taskRepository.DeleteAsync(task);
    }
}
