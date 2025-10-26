using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Application.Usecases;
using Tasks.Presentation.DTOs;
using Tasks.Presentation.Mappers;
using Tasks.Security.Config.Roles;

namespace Tasks.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(AddTask addTaskUseCase, GetTaskById getTaskByIdUseCase) : ControllerBase
{
    private readonly AddTask _addTaskUseCase = addTaskUseCase;
    private readonly GetTaskById _getTaskByIdUseCase = getTaskByIdUseCase;

    [HttpPost("add")]
    // [RolesAuthorize(nameof(TasksController), nameof(CreateTask))]
    [AllowAnonymous]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
    {

        var appRequest = TaskPresentationMapper.ToApplication(request);
        var appResponse = await _addTaskUseCase.ExecuteAsync(appRequest);
        var response = TaskPresentationMapper.ToApi(appResponse);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    // [RolesAuthorize(nameof(TasksController), nameof(GetTaskById))]
    [AllowAnonymous]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var appResponse = await _getTaskByIdUseCase.ExecuteAsync(id);
        if (appResponse == null) return NotFound();
        var response = TaskPresentationMapper.ToApi(appResponse);

        return Ok(response);
    }
}
