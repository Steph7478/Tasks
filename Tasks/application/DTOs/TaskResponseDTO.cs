using Tasks.Domain.Enums;

namespace Tasks.Application.DTOs;

public record TaskResponseDTO
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required Status CurrentStatus { get; set; }
}
