using Tasks.Application.DTOs;
using Tasks.Application.Repositories;
using Tasks.Application.Mappers;
using Tasks.Domain.Enums;
using Tasks.Domain.Repositories;
using Tasks.Domain.Services;
using DomainTask = Tasks.Domain.Entities.Task;


namespace Tasks.Application.Usecases;

public class UpdateStatusUseCase(ITaskRepository taskRepository, TaskDomainService taskDomainService) : IUpdateStatus
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly TaskDomainService _taskDomainService = taskDomainService;

    public async Task<TaskResponseDTO> ExecuteAsync(Guid id, Status newStatus)
    {
        DomainTask entity = await _taskRepository.GetByIdAsync(id);

        _taskDomainService.ChangeStatus(entity, newStatus);

        await _taskRepository.UpdateAsync(entity);

        return TaskMapper.ToDTO(entity);
    }
}
