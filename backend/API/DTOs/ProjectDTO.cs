namespace API.DTOs;

/// <summary>
/// Data Transfer Object record of project entity.
/// Only the necessary properties for project creation.
/// </summary>
public record ProjectDTO(string Name, string Customer, 
    string Executor, Guid LeaderId, DateOnly StartDate,
    DateOnly EndDate, int Priority);

