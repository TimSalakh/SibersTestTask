using API.VM;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Get(Guid id)
    {
        var targetEmployee = await _employeeRepository.GetByIdAsync(id);

        if (targetEmployee == null)
            return NotFound();

        return Ok(targetEmployee);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] EmployeeDTO employeeDTO)
    {
        var employee = new Employee
        {
            Id = new Guid(),
            Name = employeeDTO.Name,
            Surname = employeeDTO.Surname,
            Patronymic = employeeDTO.Patronymic,
            Email = employeeDTO.Email
        };
        await _employeeRepository.AddAsync(employee);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Update(Guid id, [FromBody] EmployeeDTO employeeDTO)
    {
        var targetEmployee = await _employeeRepository.GetByIdAsync(id);
        if (targetEmployee == null)
            return NotFound();

        targetEmployee.Name = employeeDTO.Name;
        targetEmployee.Surname = employeeDTO.Surname;
        targetEmployee.Patronymic = employeeDTO.Patronymic;
        targetEmployee.Email = employeeDTO.Email;

        await _employeeRepository.UpdateAsync(targetEmployee);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        var targetEmployee = await _employeeRepository.GetByIdAsync(id);
        if (targetEmployee == null)
            return NotFound();

        await _employeeRepository.DeleteAsync(id);
        return Ok();
    }
}
