namespace DAL.Models;

/// <summary>
/// Objective class which contains all properties 
/// to interact with.
/// </summary>
public class Objective
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ExecutorId { get; set; }
    public Employee? Executor { get; set; }
    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; }
    public ObjectiveStatus Status { get; set; }
    public string Description { get; set; }
    public int Priority { get; set;}
}

public enum ObjectiveStatus
{
    ToDo,
    InProgress,
    Done
}