namespace API.DTOs;

/// <summary>
/// Data Transfer Object class of objective entity.
/// Only the necessary properties for objective creation.
/// </summary>
public record struct ObjectiveDto(string name,
    Guid executorId,
    Guid projectId, int status,
    string description, int priority);