using Tasks.Infrastructure.Entities;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Infrastructure.Mappers
{
    public static class TaskMapper
    {
        // Entity -> Domain
        public static DomainTask ToDomain(TaskEntity entity)
        {
            DomainTask domain = new(entity.Title, entity.Description);
            typeof(DomainTask).GetProperty("Id")!.SetValue(domain, entity.Id);
            typeof(DomainTask).GetProperty("CreatedAt")!.SetValue(domain, entity.CreatedAt);
            typeof(DomainTask).GetProperty("CurrentStatus")!.SetValue(domain, entity.CurrentStatus);
            return domain;
        }

        // Domain -> Entity
        public static TaskEntity ToEntity(DomainTask domain)
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
