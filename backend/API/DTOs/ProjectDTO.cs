namespace API.VM;

public record struct ProjectDTO(string Name, string Customer, 
    string Executor, Guid LeaderId, DateOnly StartDate,
    DateOnly EndDate, int Priority);

