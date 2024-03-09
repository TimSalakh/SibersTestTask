using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace API.VM;

public class EmployeeDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string Patronymic { get; set; }

    [Required]
    public string Email { get; set; }
}
