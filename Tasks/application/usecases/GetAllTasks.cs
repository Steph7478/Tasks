using Tasks.Application.DTOs;
using Tasks.Application.Mappers;
using Tasks.Domain.Repositories;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Application.Usecases
{
    public class GetAllTasksUseCase(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<IEnumerable<TaskResponseDTO>> ExecuteAsync()
        {
            List<DomainTask> tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(TaskMapper.ToDTO);
        }
    }
}
