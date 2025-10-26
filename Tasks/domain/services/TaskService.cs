using DomainTask = Tasks.Domain.Entities.Task;
using Tasks.Domain.Enums;

namespace Tasks.Domain.Services
{
    public class TaskDomainService
    {
        public void ChangeStatus(DomainTask task, Status newStatus)
        {
            task.UpdateStatus(newStatus);
        }

        public void UpdateTask(DomainTask task, string? newTitle = null, string? newDescription = null)
        {
            task.Update(newTitle, newDescription);
        }

        public void DeleteTask(DomainTask task)
        {
            Console.WriteLine($"Task '{task.Title}' will be deleted.");
        }
    }
}
