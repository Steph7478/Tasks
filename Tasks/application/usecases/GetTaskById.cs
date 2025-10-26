using Tasks.Domain.Repositories;
using Tasks.Application.DTOs;
using Tasks.Application.Mappers;

namespace Tasks.Application.Usecases;

public class GetTaskById(ITaskRepository taskRepository)
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<TaskResponseDTO?> ExecuteAsync(Guid id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null) return null;

        return TaskMapper.ToDTO(task);
    }
}
