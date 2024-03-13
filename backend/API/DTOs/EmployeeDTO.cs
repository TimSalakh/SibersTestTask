namespace API.DTOs;

/// <summary>
/// Data Transfer Object class of employee entity.
/// Only the necessary properties for employee creation.
/// </summary>
public record struct EmployeeDto(string Name, string Surname, 
    string Patronymic, string Email);

