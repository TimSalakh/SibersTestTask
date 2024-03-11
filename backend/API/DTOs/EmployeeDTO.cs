namespace API.DTOs;

public record struct EmployeeDto(string Name, string Surname, 
    string Patronymic, string Email);

