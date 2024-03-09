using API.VM;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Project>>> GetAll()
    {
        var projects = await _projectRepository.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> Get(Guid id)
    {
        var targetProject = await _projectRepository.GetByIdAsync(id);

        if (targetProject == null)
            return NotFound();

        return Ok(targetProject);
    }

    [HttpGet("Employees/{id}")]
    public async Task<ActionResult<List<Employee>>> GetEmployees(Guid id)
    {
        var employees = await _projectRepository.GetEmployees(id);

        return Ok(employees);
    }


    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ProjectDTO projectDTO)
    {
        var project = new Project
        { 
            Name = projectDTO.Name,
            Customer = projectDTO.Customer,
            Executor = projectDTO.Executor,
            LeaderId = projectDTO.LeaderId,
            StartDate = projectDTO.StartDate,
            EndDate = projectDTO.EndDate,
            Priority = projectDTO.Priority
        };
        await _projectRepository.AddAsync(project);
        return Ok();
    }

    [HttpPost("{projectId}/{employeeId}")]
    public async Task<ActionResult> AddEmployee(Guid projectId, Guid employeeId)
    {
        await _projectRepository.AddEmployee(projectId, employeeId);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Update(Guid id, [FromBody] ProjectDTO projectDTO)
    {
        var targetProject = await _projectRepository.GetByIdAsync(id);
        if (targetProject == null)
            return NotFound();

        targetProject.Name = projectDTO.Name;
        targetProject.Customer = projectDTO.Customer;
        targetProject.Executor = projectDTO.Executor;
        targetProject.LeaderId = projectDTO.LeaderId;
        targetProject.StartDate = projectDTO.StartDate;
        targetProject.EndDate = projectDTO.EndDate;
        targetProject.Priority = projectDTO.Priority;

        await _projectRepository.UpdateAsync(targetProject);
        return Ok();
    }

    [HttpDelete]   
    public async Task<ActionResult> Delete(Guid id)
    {
        var targetProject = await _projectRepository.GetByIdAsync(id);
        if (targetProject == null)
            return NotFound();

        await _projectRepository.DeleteAsync(id);
        return Ok();
    }

    [HttpDelete("{projectId}/{employeeId}")]
    public async Task<ActionResult> DeleteEmployee(Guid projectId, Guid employeeId)
    {
        await _projectRepository.DeleteEmployee(projectId, employeeId);
        return Ok();
    }
}
