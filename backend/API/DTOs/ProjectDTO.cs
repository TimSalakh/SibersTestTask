namespace API.DTOs;

public record struct ProjectDTO(string Name, string Customer, 
    string Executor, Guid LeaderId, DateOnly StartDate,
    DateOnly EndDate, int Priority);

