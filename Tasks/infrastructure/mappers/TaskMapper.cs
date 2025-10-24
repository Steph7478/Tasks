using Tasks.Infrastructure.Entities;
using Task = Tasks.Domain.Entities.Task;

namespace Tasks.Infrastructure.Mappers
{
    public static class TaskMapper
    {
        // Infra → Domain
        public static Task ToDomain(TaskEntity entity)
        {
            var task = new Task(entity.Title, entity.Description);

            typeof(Task).GetProperty("Id")!.SetValue(task, entity.Id);
            typeof(Task).GetProperty("CreatedAt")!.SetValue(task, entity.CreatedAt);
            typeof(Task).GetProperty("CurrentStatus")!.SetValue(task, entity.CurrentStatus);

            return task;
        }

        // Domain → Infra
        public static TaskEntity ToEntity(Task domain)
        {
            return new TaskEntity
            {
                Id = domain.Id,
                Title = domain.Title,
                Description = domain.Description,
                CreatedAt = domain.CreatedAt,
                CurrentStatus = domain.CurrentStatus
            };
        }
    }
}
