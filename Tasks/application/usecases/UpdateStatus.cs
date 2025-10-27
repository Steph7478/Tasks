using Tasks.Application.DTOs;
using Tasks.Application.Mappers;
using Tasks.Domain.Enums;
using Tasks.Domain.Repositories;
using Tasks.Domain.Services;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Application.Usecases;

public class UpdateStatusUseCase
{
    private readonly ITaskRepository _taskRepository;
    private readonly TaskDomainService _taskDomainService;

    public UpdateStatusUseCase(ITaskRepository taskRepository, TaskDomainService taskDomainService)
    {
        _taskRepository = taskRepository;
        _taskDomainService = taskDomainService;
    }

    public async Task<TaskResponseDTO> ExecuteAsync(DomainTask task, Status newStatus)
    {
        _taskDomainService.ChangeStatus(task, newStatus);

        await _taskRepository.UpdateAsync(task);

        return TaskMapper.ToDTO(task);
    }
}
