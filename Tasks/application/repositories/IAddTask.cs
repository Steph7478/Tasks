using Tasks.Application.DTOs;

namespace Tasks.Application.Repositories
{
    public interface IAddTask
    {
        Task<TaskResponseDTO> ExecuteAsync(TaskRequestDTO request);
    }

}