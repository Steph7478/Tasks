using Tasks.Application.DTOs;

namespace Tasks.Application.Repositories
{
    public interface IGetTaskById
    {
        Task<TaskResponseDTO> ExecuteAsync(Guid id);
    }

}