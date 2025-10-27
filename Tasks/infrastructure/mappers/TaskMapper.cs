using Tasks.Infrastructure.Entities;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Infrastructure.Mappers
{
    public static class TaskMapper
    {
        private static readonly Dictionary<Guid, TaskEntity> _tracked = new();

        public static DomainTask ToDomainTracked(TaskEntity entity)
        {
            var domain = new DomainTask(entity.Title, entity.Description);
            typeof(DomainTask).GetProperty("Id")!.SetValue(domain, entity.Id);
            typeof(DomainTask).GetProperty("CreatedAt")!.SetValue(domain, entity.CreatedAt);
            typeof(DomainTask).GetProperty("CurrentStatus")!.SetValue(domain, entity.CurrentStatus);

            _tracked[domain.Id] = entity;

            return domain;
        }

        public static TaskEntity GetTrackedEntity(DomainTask domain)
        {
            if (_tracked.TryGetValue(domain.Id, out var entity))
                return entity;

            throw new InvalidOperationException("DomainTask não está sendo rastreado");
        }

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