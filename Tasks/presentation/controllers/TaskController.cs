using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Application.Usecases;
using Tasks.Presentation.DTOs;
using Tasks.Presentation.Mappers;

namespace Tasks.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(AddTask addTaskUseCase, GetTaskById getTaskByIdUseCase, UpdateTaskUseCase updateTaskUseCase) : ControllerBase
{
    private readonly AddTask _addTaskUseCase = addTaskUseCase;
    private readonly GetTaskById _getTaskByIdUseCase = getTaskByIdUseCase;
    private readonly UpdateTaskUseCase _updateTaskUseCase = updateTaskUseCase;

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

    [HttpPut("update")]
    // [RolesAuthorize(nameof(TasksController), nameof(UpdateTask))]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskRequest request)
    {
        var appRequest = TaskPresentationMapper.ToApplication(request);
        var appResponse = await _updateTaskUseCase.ExecuteAsync(id, appRequest);

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
