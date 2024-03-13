using API.DTOs;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// REST API objective's class which contains GET, POST, PUT and DELETE
/// methods to provide interaction frontend with backed parts 
/// of the application. Only necessary methods, nothing special.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ObjectiveController : Controller
{
    private readonly IObjectiveRepository _objectiveRepository;

    public ObjectiveController(IObjectiveRepository objectiveRepository)
    {
        _objectiveRepository = objectiveRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Objective>>> GetAll()
    {
        var objectives = await _objectiveRepository.GetAllAsync();
        return Ok(objectives);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Objective>> Get(Guid id)
    {
        var targetObjective = await _objectiveRepository.GetByIdAsync(id);
        return Ok(targetObjective);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ObjectiveDto objectiveDto)
    {
        var objective = new Objective
        {
            Id = Guid.NewGuid(),
            Name = objectiveDto.name,
            ExecutorId = objectiveDto.executorId,
            ProjectId = objectiveDto.projectId,
            Status = ObjectiveStatus.ToDo,
            Description = objectiveDto.description,
            Priority = objectiveDto.priority
        };
        await _objectiveRepository.AddAsync(objective);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Update(Guid id, [FromBody] ObjectiveDto objectiveDto)
    {
        var targetObjective = await _objectiveRepository.GetByIdAsync(id);
        targetObjective.Name = objectiveDto.name;
        targetObjective.Status = (ObjectiveStatus)objectiveDto.status;
        targetObjective.Description = objectiveDto.description;
        targetObjective.Priority = objectiveDto.priority;
        await _objectiveRepository.UpdateAsync(targetObjective);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        var targetObjective = await _objectiveRepository.GetByIdAsync(id);
        await _objectiveRepository.DeleteAsync(id);
        return Ok();
    }
}
