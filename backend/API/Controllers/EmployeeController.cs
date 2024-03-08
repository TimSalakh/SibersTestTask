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
    public async Task<ActionResult<List<Employee>>> GetAllAssync()
    {
        return await _employeeRepository.GetAllAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<Employee>> GetAsync(Guid id)
    {
        var targetEmployee = await _employeeRepository.GetByIdAsync(id);

        if (targetEmployee == null)
            return NotFound();

        return Ok(targetEmployee);
    }

    //[HttpGet("id")]
    //public async Task<ActionResult<List<Project>?>> GetProjectsAsync(Guid id)
    //{
    //    return await _employeeRepository.GetProjectsAsync(id);
    //}

    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] string name, string surname, string patronymic)
    {
        await _employeeRepository.AddAsync(name, surname, patronymic);
        return Ok();
    }

    //[HttpPut]
    //public async Task<ActionResult> UpdateAsync(Employee employee)
    //{
    //    await _employeeRepository.UpdateAsync(employee);
    //    return NoContent();
    //}

    //[HttpDelete]
    //public async Task<ActionResult> DeleteAsync(Guid id)
    //{
    //    await _employeeRepository.DeleteAsync(id);
    //    return NoContent();
    //}
}
