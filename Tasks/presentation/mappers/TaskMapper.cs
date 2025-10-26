using Tasks.Application.DTOs;
using Tasks.Presentation.DTOs;

namespace Tasks.Presentation.Mappers
{
    public static class TaskPresentationMapper
    {
        // Mapper de Presentation -> Application
        public static TaskRequestDTO ToApplication(this TaskRequest request)
        {
            return new TaskRequestDTO
            {
                Title = request.Title,
                Description = request.Description
            };
        }

        // Mapper de Application -> Presentation
        public static TaskResponse ToApi(this TaskResponseDTO response)
        {
            return new TaskResponse(
                response.Id,
                response.Title,
                response.Description,
                response.CreatedAt,
                response.CurrentStatus
            );
        }
    }
}
