using Tasks.Domain.Repositories;
using Tasks.Application.DTOs;
using Tasks.Application.Mappers;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Application.Usecases;

public class AddTask(ITaskRepository taskRepository)
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<TaskResponseDTO> ExecuteAsync(TaskRequestDTO request)
    {
        DomainTask taskEntity = TaskMapper.ToEntity(request);
        await _taskRepository.AddAsync(taskEntity);

        return TaskMapper.ToDTO(taskEntity);
    }
}
