namespace DAL.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public string Customer { get; set; }
    public string Executor { get; set; }
    public List<Employee> Employees { get; set; }
    public Guid LeaderId { get; set; }
    public Employee Leader { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int Priority { get; set; }
}
