using Tasks.Domain.Repositories;
using Tasks.Application.DTOs;
using Tasks.Application.Mappers;
using DomainTask = Tasks.Domain.Entities.Task;
using Tasks.Application.Repositories;

namespace Tasks.Application.Usecases;

public class GetTaskById(ITaskRepository taskRepository) : IGetTaskById
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<TaskResponseDTO> ExecuteAsync(Guid id)
    {
        DomainTask task = await _taskRepository.GetByIdAsync(id);

        return TaskMapper.ToDTO(task);
    }
}
