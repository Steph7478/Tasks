using Tasks.Domain.Repositories;
using Tasks.Domain.Services;
using Tasks.Application.DTOs;
using Tasks.Application.Mappers;

namespace Tasks.Application.Usecases
{
    public class UpdateTaskUseCase(ITaskRepository taskRepository, TaskDomainService taskDomainService)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly TaskDomainService _taskDomainService = taskDomainService;

        public async Task<TaskResponseDTO> ExecuteAsync(Guid id, TaskRequestDTO request)
        {
            var task = await _taskRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Task not found");

            _taskDomainService.UpdateTask(task, request.Title, request.Description);

            await _taskRepository.UpdateAsync(task);

            return TaskMapper.ToDTO(task);
        }
    }

}
