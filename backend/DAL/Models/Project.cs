namespace DAL.Models;

/// <summary>
/// Project class which contains all properties 
/// to interact with.
/// </summary>
public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Customer { get; set; }
    public Guid? LeaderId { get; set; }
    public Employee? Leader { get; set; }
    public string Executor { get; set; }
    public ICollection<Employee>? Employees { get; set; } = [];
    public ICollection<Objective>? Objectives { get; set; } = [];
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int Priority { get; set; }
}
