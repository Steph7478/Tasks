using Tasks.Domain.Repositories;
using Tasks.Domain.Services;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Application.Usecases;

public class DeleteTaskUseCase(ITaskRepository taskRepository, TaskDomainService taskDomainService)
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly TaskDomainService _taskDomainService = taskDomainService;

    public async Task<bool> ExecuteAsync(Guid id)
    {
        DomainTask task = await _taskRepository.GetByIdAsync(id);
        if (task == null) return false;

        _taskDomainService.ValidateDelete(task);
        await _taskRepository.DeleteAsync(task);

        return true;
    }
}

