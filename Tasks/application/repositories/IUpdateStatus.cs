using Tasks.Application.DTOs;
using Tasks.Domain.Enums;

namespace Tasks.Application.Repositories
{
    public interface IUpdateStatus
    {
        Task<TaskResponseDTO> ExecuteAsync(Guid id, Status newStatus);
    }

}