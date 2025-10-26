using Tasks.Domain.Enums;

namespace Tasks.Presentation.DTOs;

public record TaskResponse(
    Guid Id,
    string Title,
    string Description,
    DateTime CreatedAt,
    Status CurrentStatus
);
