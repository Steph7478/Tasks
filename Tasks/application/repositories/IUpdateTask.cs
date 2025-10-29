using Tasks.Application.DTOs;

namespace Tasks.Application.Repositories
{
    public interface IUpdateTask
    {
        Task<TaskResponseDTO> ExecuteAsync(Guid id, TaskRequestDTO request);
    }

}