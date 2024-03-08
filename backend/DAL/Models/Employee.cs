namespace DAL.Models;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public List<Project> Projects { get; set; } = [];
}