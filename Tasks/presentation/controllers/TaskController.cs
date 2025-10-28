using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Application.DTOs;
using Tasks.Application.Usecases;
using Tasks.Presentation.DTOs;
using Tasks.Presentation.Mappers;

namespace Tasks.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(AddTask addTaskUseCase, GetTaskById getTaskByIdUseCase, UpdateTaskUseCase updateTaskUseCase, UpdateStatusUseCase completeTask, DeleteTaskUseCase deleteTaskUseCase) : ControllerBase
{
    private readonly AddTask _addTaskUseCase = addTaskUseCase;
    private readonly GetTaskById _getTaskByIdUseCase = getTaskByIdUseCase;
    private readonly UpdateTaskUseCase _updateTaskUseCase = updateTaskUseCase;
    private readonly UpdateStatusUseCase _completeTask = completeTask;
    private readonly DeleteTaskUseCase _deleteTask = deleteTaskUseCase;

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        TaskResponseDTO appResponse = await _getTaskByIdUseCase.ExecuteAsync(id);
        if (appResponse == null) return NotFound();
        TaskResponse response = TaskPresentationMapper.ToApi(appResponse);

        return Ok(response);
    }

    [HttpPost("add")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
    {
        TaskRequestDTO appRequest = TaskPresentationMapper.ToApplication(request);
        TaskResponseDTO appResponse = await _addTaskUseCase.ExecuteAsync(appRequest);
        TaskResponse response = TaskPresentationMapper.ToApi(appResponse);

        return Ok(response);
    }

    [HttpPut("{id:guid}/update")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskRequest request)
    {
        TaskRequestDTO appRequest = TaskPresentationMapper.ToApplication(request);
        TaskResponseDTO appResponse = await _updateTaskUseCase.ExecuteAsync(id, appRequest);

        TaskResponse response = TaskPresentationMapper.ToApi(appResponse);

        return Ok(response);
    }

    [HttpPut("{id:guid}/status")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateTaskStatus(Guid id, [FromBody] UpdateStatusRequest request)
    {
        TaskResponseDTO appResponse = await _completeTask.ExecuteAsync(id, request.CurrentStatus);

        TaskResponse response = TaskPresentationMapper.ToApi(appResponse);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        bool deleted = await _deleteTask.ExecuteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

}

// for flexibility public async Task<IActionResult> GetTaskById([FromRoute(Name = "id")] + Guid id = idFromRoute ?? idFromQuery ?? throw new ArgumentException("ID not found");

// for future auth [RolesAuthorize(nameof(TasksController), nameof(Action))]