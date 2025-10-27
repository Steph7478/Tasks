using DomainTask = Tasks.Domain.Entities.Task;
using Tasks.Domain.Enums;

namespace Tasks.Domain.Services
{
    public class TaskDomainService
    {
        public void ChangeStatus(DomainTask task, Status newStatus)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");

            if (!Enum.IsDefined(newStatus))
                throw new InvalidOperationException("Invalid status.");

            task.UpdateStatus(newStatus);
        }

        public void UpdateTask(DomainTask task, string? newTitle = null, string? newDescription = null)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");

            if (task.CurrentStatus == Status.Completed)
                throw new InvalidOperationException("Cannot update a completed task.");

            task.Update(newTitle, newDescription);
        }

        public void ValidateDelete(DomainTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");

            if (task.CurrentStatus == Status.Completed)
                throw new InvalidOperationException("Cannot delete a completed task.");
        }
    }
}
