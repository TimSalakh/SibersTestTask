namespace API.DTOs;

/// <summary>
/// Data Transfer Object class of project entity.
/// Only the necessary properties for project creation.
/// </summary>
public record struct ProjectDTO(string Name, string Customer, 
    string Executor, Guid LeaderId, DateOnly StartDate,
    DateOnly EndDate, int Priority);

