namespace API.DTOs;

/// <summary>
/// Data Transfer Object record of objective entity.
/// Only the necessary properties for objective creation.
/// </summary>
public record ObjectiveDto(string name,
    Guid executorId,
    Guid projectId, int status,
    string description, int priority);