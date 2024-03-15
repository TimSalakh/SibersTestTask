namespace API.DTOs;

/// <summary>
/// Data Transfer Object record of employee entity.
/// Only the necessary properties for employee creation.
/// </summary>
public record EmployeeDto(string Name, string Surname, 
    string Patronymic, string Email);

