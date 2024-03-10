namespace API.DTOs;

public record struct ObjectiveDto(string name,
    Guid creatorId, Guid executorId,
    Guid projectId, int status,
    string description, int priority);