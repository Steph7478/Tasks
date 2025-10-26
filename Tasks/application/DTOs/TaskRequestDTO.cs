using Tasks.Domain.Enums;

namespace Tasks.Application.DTOs;

public record TaskRequestDTO
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Status CurrentStatus { get; set; } = Status.Pending;
}
