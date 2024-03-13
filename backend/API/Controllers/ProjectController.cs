using API.DTOs;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// REST API employee's class which contains GET, POST, PUT and DELETE
/// methods to provide interaction frontend with backed part 
/// of the application. Only necessary methods, nothing special.
/// </summary>
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
    public async Task<ActionResult<IEnumerable<Project>>> GetAll()
    {
        var projects = await _projectRepository.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Project>> Get(Guid id)
    {
        var targetProject = await _projectRepository.GetByIdAsync(id);

        if (targetProject == null)
            return NotFound();

        return Ok(targetProject);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ProjectDTO projectDto)
    {
        var project = new Project
        { 
            Id = Guid.NewGuid(),
            Name = projectDto.Name,
            Customer = projectDto.Customer,
            Executor = projectDto.Executor,
            LeaderId = projectDto.LeaderId,
            StartDate = projectDto.StartDate,
            EndDate = projectDto.EndDate,
            Priority = projectDto.Priority
        };
        await _projectRepository.AddAsync(project);
        return Ok();
    }

    [HttpPost("{projectId:guid}/{employeeId:guid}")]
    public async Task<ActionResult> AddEmployee(Guid projectId, Guid employeeId)
    {
        await _projectRepository.AddEmployee(projectId, employeeId);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Update(Guid id, [FromBody] ProjectDTO projectDto)
    {
        var targetProject = await _projectRepository.GetByIdAsync(id);
        targetProject.Name = projectDto.Name;
        targetProject.Customer = projectDto.Customer;
        targetProject.Executor = projectDto.Executor;
        targetProject.LeaderId = projectDto.LeaderId;
        targetProject.StartDate = projectDto.StartDate;
        targetProject.EndDate = projectDto.EndDate;
        targetProject.Priority = projectDto.Priority;

        await _projectRepository.UpdateAsync(targetProject);
        return Ok();
    }

    [HttpDelete]   
    public async Task<ActionResult> Delete(Guid id)
    {
        var targetProject = await _projectRepository.GetByIdAsync(id);
        await _projectRepository.DeleteAsync(id);
        return Ok();
    }

    [HttpDelete("{projectId:guid}/{employeeId:guid}")]
    public async Task<ActionResult> DeleteEmployee(Guid projectId, Guid employeeId)
    {
        await _projectRepository.DeleteEmployee(projectId, employeeId);
        return Ok();
    }
}
