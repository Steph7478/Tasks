using DomainTask = Tasks.Domain.Entities.Task;
using Tasks.Domain.Enums;

namespace Tasks.Domain.Services
{
    public class TaskDomainService
    {
        public void ChangeStatus(DomainTask task, Status newStatus)
        {
            if (task.CurrentStatus == Status.Completed)
                throw new InvalidOperationException("Cannot change status after completion.");

            task.UpdateStatus(newStatus);
        }
    }
}
