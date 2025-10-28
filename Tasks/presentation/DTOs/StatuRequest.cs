using Tasks.Domain.Enums;

namespace Tasks.Presentation.DTOs
{
    public record UpdateStatusRequest
    {
        public Status CurrentStatus { get; set; }
    }
}