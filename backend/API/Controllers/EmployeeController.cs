using API.DTOs;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
    {
        var employees = await _employeeRepository.GetAllAsync();       
        return Ok(employees);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Employee>> Get(Guid id)
    {
        var targetEmployee = await _employeeRepository.GetByIdAsync(id);

        if (targetEmployee == null)
            return NotFound();

        return Ok(targetEmployee);
    }

    [HttpGet("Objectives/{id:guid}")]
    public async Task<ActionResult<IEnumerable<Objective>>> GetObjectives(Guid id)
    {
        var objectives = await _employeeRepository.GetObjectives(id);
        return Ok(objectives);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] EmployeeDto employeeDto)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = employeeDto.Name,
            Surname = employeeDto.Surname,
            Patronymic = employeeDto.Patronymic,
            Email = employeeDto.Email
        };
        await _employeeRepository.AddAsync(employee);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Update(Guid id, [FromBody] EmployeeDto employeeDto)
    {
        var targetEmployee = await _employeeRepository.GetByIdAsync(id);
        if (targetEmployee == null)
            return NotFound();

        targetEmployee.Name = employeeDto.Name;
        targetEmployee.Surname = employeeDto.Surname;
        targetEmployee.Patronymic = employeeDto.Patronymic;
        targetEmployee.Email = employeeDto.Email;

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
