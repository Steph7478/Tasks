using Tasks.Application.DTOs;

namespace Tasks.Application.Repositories
{
    public interface IGetAllTasks
    {
        Task<IEnumerable<TaskResponseDTO>> ExecuteAsync();
    }

}