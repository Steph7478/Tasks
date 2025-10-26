using Tasks.Application.DTOs;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Application.Mappers;

public static class TaskMapper
{
    // DTO -> Entity
    public static DomainTask ToEntity(TaskRequestDTO dto)
    {
        return new DomainTask(dto.Title, dto.Description);
    }

    // Entity -> DTO
    public static TaskResponseDTO ToDTO(DomainTask task)
    {
        return new TaskResponseDTO
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedAt = task.CreatedAt,
            CurrentStatus = task.CurrentStatus
        };
    }
}
